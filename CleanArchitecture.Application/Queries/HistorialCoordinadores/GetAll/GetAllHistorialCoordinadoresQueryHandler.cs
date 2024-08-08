using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.HistorialCoordinadores.GetAll;

public sealed class GetAllHistorialCoordinadoresQueryHandler :
    IRequestHandler<GetAllHistorialCoordinadoresQuery, PagedResult<HistorialCoordinadorViewModel>>
{
    private readonly ISortingExpressionProvider<HistorialCoordinadorViewModel, HistorialCoordinador> _sortingExpressionProvider;
    private readonly IHistorialCoordinadorRepository _historialcoordinadorRepository;

    public GetAllHistorialCoordinadoresQueryHandler(
        IHistorialCoordinadorRepository historialcoordinadorRepository,
        ISortingExpressionProvider<HistorialCoordinadorViewModel, HistorialCoordinador> sortingExpressionProvider)
    {
        _historialcoordinadorRepository = historialcoordinadorRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<HistorialCoordinadorViewModel>> Handle(
        GetAllHistorialCoordinadoresQuery request,
        CancellationToken cancellationToken)
    {
        var historialcoordinadoresQuery = _historialcoordinadorRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        /*
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            historialcoordinadoresQuery = historialcoordinadoresQuery.Where(historialcoordinador =>
                historialcoordinador.EventoId.Contains(request.SearchTerm));
        }
        */
        var totalCount = await historialcoordinadoresQuery.CountAsync(cancellationToken);

        historialcoordinadoresQuery = historialcoordinadoresQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var historialcoordinadores = await historialcoordinadoresQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(historialcoordinador => HistorialCoordinadorViewModel.FromHistorialCoordinador(historialcoordinador))
            .ToListAsync(cancellationToken);

        return new PagedResult<HistorialCoordinadorViewModel>(
            totalCount, historialcoordinadores, request.Query.Page, request.Query.PageSize);
    }
}