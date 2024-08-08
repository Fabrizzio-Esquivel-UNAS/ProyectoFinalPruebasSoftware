using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Facultades;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Facultades.GetAll;

public sealed class GetAllFacultadesQueryHandler :
    IRequestHandler<GetAllFacultadesQuery, PagedResult<FacultadViewModel>>
{
    private readonly ISortingExpressionProvider<FacultadViewModel, Facultad> _sortingExpressionProvider;
    private readonly IFacultadRepository _facultadRepository;

    public GetAllFacultadesQueryHandler(
        IFacultadRepository facultadRepository,
        ISortingExpressionProvider<FacultadViewModel, Facultad> sortingExpressionProvider)
    {
        _facultadRepository = facultadRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<FacultadViewModel>> Handle(
        GetAllFacultadesQuery request,
        CancellationToken cancellationToken)
    {
        var facultadesQuery = _facultadRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            //.Include(x => x.Users.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            facultadesQuery = facultadesQuery.Where(facultad =>
                facultad.Nombre.Contains(request.SearchTerm));
        }

        var totalCount = await facultadesQuery.CountAsync(cancellationToken);

        facultadesQuery = facultadesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var facultades = await facultadesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(facultad => FacultadViewModel.FromFacultades(facultad))
            .ToListAsync(cancellationToken);

        return new PagedResult<FacultadViewModel>(
            totalCount, facultades, request.Query.Page, request.Query.PageSize);
    }
}