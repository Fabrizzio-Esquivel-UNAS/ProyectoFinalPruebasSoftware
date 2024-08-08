using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Escuelas;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Escuelas.GetAll;

public sealed class GetAllEscuelasQueryHandler :
    IRequestHandler<GetAllEscuelasQuery, PagedResult<EscuelaViewModel>>
{
    private readonly ISortingExpressionProvider<EscuelaViewModel, Escuela> _sortingExpressionProvider;
    private readonly IEscuelaRepository _escuelaRepository;

    public GetAllEscuelasQueryHandler(
        IEscuelaRepository escuelaRepository,
        ISortingExpressionProvider<EscuelaViewModel, Escuela> sortingExpressionProvider)
    {
        _escuelaRepository = escuelaRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<EscuelaViewModel>> Handle(
        GetAllEscuelasQuery request,
        CancellationToken cancellationToken)
    {
        var escuelasQuery = _escuelaRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            //.Include(x => x.Users.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            escuelasQuery = escuelasQuery.Where(escuela =>
                escuela.Nombre.Contains(request.SearchTerm));
        }

        var totalCount = await escuelasQuery.CountAsync(cancellationToken);

        escuelasQuery = escuelasQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var escuelas = await escuelasQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(escuela => EscuelaViewModel.FromEscuelas(escuela))
            .ToListAsync(cancellationToken);

        return new PagedResult<EscuelaViewModel>(
            totalCount, escuelas, request.Query.Page, request.Query.PageSize);
    }
}