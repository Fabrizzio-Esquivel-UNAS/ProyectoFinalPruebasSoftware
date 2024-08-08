using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Citas;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Citas.GetAll;

public sealed class GetAllCitasQueryHandler :
    IRequestHandler<GetAllCitasQuery, PagedResult<CitaViewModel>>
{
    private readonly ISortingExpressionProvider<CitaViewModel, Cita> _sortingExpressionProvider;
    private readonly ICitaRepository _citaRepository;

    public GetAllCitasQueryHandler(
        ICitaRepository citaRepository,
        ISortingExpressionProvider<CitaViewModel, Cita> sortingExpressionProvider)
    {
        _citaRepository = citaRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CitaViewModel>> Handle(
        GetAllCitasQuery request,
        CancellationToken cancellationToken)
    {
        var citasQuery = _citaRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            //.Include(x => x.Users.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            citasQuery = citasQuery.Where(cita =>
                cita.EventoId.Contains(request.SearchTerm));
        }

        var totalCount = await citasQuery.CountAsync(cancellationToken);

        citasQuery = citasQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var citas = await citasQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(cita => CitaViewModel.FromCita(cita))
            .ToListAsync(cancellationToken);

        return new PagedResult<CitaViewModel>(
            totalCount, citas, request.Query.Page, request.Query.PageSize);
    }
}