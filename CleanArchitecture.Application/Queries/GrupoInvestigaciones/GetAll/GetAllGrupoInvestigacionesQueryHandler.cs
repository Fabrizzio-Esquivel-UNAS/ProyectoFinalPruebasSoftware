using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.GruposInvestigacion;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Queries.GruposInvestigacion.GetAll;

public sealed class GetAllGruposInvestigacionQueryHandler :
    IRequestHandler<GetAllGruposInvestigacionQuery, PagedResult<GrupoInvestigacionViewModel>>
{
    private readonly ISortingExpressionProvider<GrupoInvestigacionViewModel, GrupoInvestigacion> _sortingExpressionProvider;
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;
    private readonly IHistorialCoordinadorRepository _historialCoordinadorRepository;

    public GetAllGruposInvestigacionQueryHandler(
        IGrupoInvestigacionRepository grupoinvestigacionRepository,
        IHistorialCoordinadorRepository historialCoordinadorRepository,
        ISortingExpressionProvider<GrupoInvestigacionViewModel, GrupoInvestigacion> sortingExpressionProvider)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
        _historialCoordinadorRepository = historialCoordinadorRepository;
    }

    public async Task<PagedResult<GrupoInvestigacionViewModel>> Handle(
        GetAllGruposInvestigacionQuery request,
        CancellationToken cancellationToken)
    {
        var gruposInvestigacionQuery = _grupoinvestigacionRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            //.Include(x => x.Users.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            gruposInvestigacionQuery = gruposInvestigacionQuery.Where(grupoinvestigacion =>
                grupoinvestigacion.Nombre.Contains(request.SearchTerm));
        }

        var totalCount = await gruposInvestigacionQuery.CountAsync(cancellationToken);

        gruposInvestigacionQuery = gruposInvestigacionQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var gruposInvestigacion = await gruposInvestigacionQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .ToListAsync(cancellationToken);

        var gruposInvestigacionViewModelList = new List<GrupoInvestigacionViewModel>();

        foreach (var grupoinvestigacion in gruposInvestigacion)
        {
            var grupoInvestigacionViewModel = GrupoInvestigacionViewModel.FromGrupoInvestigacion(grupoinvestigacion);
            var historialCoordinador = await _historialCoordinadorRepository.GetLatestActiveCoordinadorByGrupoInvestigacionIdAsync(grupoinvestigacion.Id);

            if (historialCoordinador != null)
            {
                grupoInvestigacionViewModel.CoordinadorUserId = historialCoordinador.UserId;
            }

            gruposInvestigacionViewModelList.Add(grupoInvestigacionViewModel);
        }

        return new PagedResult<GrupoInvestigacionViewModel>(
            totalCount, gruposInvestigacionViewModelList, request.Query.Page, request.Query.PageSize);
    }
}