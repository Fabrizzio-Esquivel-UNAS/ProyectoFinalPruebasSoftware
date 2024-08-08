using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.LineasInvestigacion.GetAll;

public sealed class GetAllLineasInvestigacionQueryHandler :
    IRequestHandler<GetAllLineasInvestigacionQuery, PagedResult<LineaInvestigacionViewModel>>
{
    private readonly ISortingExpressionProvider<LineaInvestigacionViewModel, LineaInvestigacion> _sortingExpressionProvider;
    private readonly ILineaInvestigacionRepository _lineainvestigacionRepository;

    public GetAllLineasInvestigacionQueryHandler(
        ILineaInvestigacionRepository lineainvestigacionRepository,
        ISortingExpressionProvider<LineaInvestigacionViewModel, LineaInvestigacion> sortingExpressionProvider)
    {
        _lineainvestigacionRepository = lineainvestigacionRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<LineaInvestigacionViewModel>> Handle(
        GetAllLineasInvestigacionQuery request,
        CancellationToken cancellationToken)
    {
        var lineasInvestigacionQuery = _lineainvestigacionRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            //.Include(x => x.Users.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            lineasInvestigacionQuery = lineasInvestigacionQuery.Where(lineainvestigacion =>
                lineainvestigacion.Nombre.Contains(request.SearchTerm));
        }

        var totalCount = await lineasInvestigacionQuery.CountAsync(cancellationToken);

        lineasInvestigacionQuery = lineasInvestigacionQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var lineasInvestigacion = await lineasInvestigacionQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(lineainvestigacion => LineaInvestigacionViewModel.FromLineaInvestigacion(lineainvestigacion))
            .ToListAsync(cancellationToken);

        return new PagedResult<LineaInvestigacionViewModel>(
            totalCount, lineasInvestigacion, request.Query.Page, request.Query.PageSize);
    }
}