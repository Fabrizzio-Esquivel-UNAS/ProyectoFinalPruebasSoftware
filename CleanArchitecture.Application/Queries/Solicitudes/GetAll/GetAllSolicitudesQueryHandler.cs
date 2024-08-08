using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Solicitudes.GetAll;

public sealed class GetAllSolicitudesQueryHandler :
    IRequestHandler<GetAllSolicitudesQuery, PagedResult<SolicitudViewModel>>
{
    private readonly ISortingExpressionProvider<SolicitudViewModel, Solicitud> _sortingExpressionProvider;
    private readonly ISolicitudRepository _solicitudRepository;

    public GetAllSolicitudesQueryHandler(
        ISolicitudRepository solicitudRepository,
        ISortingExpressionProvider<SolicitudViewModel, Solicitud> sortingExpressionProvider)
    {
        _solicitudRepository = solicitudRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<SolicitudViewModel>> Handle(
        GetAllSolicitudesQuery request,
        CancellationToken cancellationToken)
    {
        var solicitudesQuery = _solicitudRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            //.Include(x => x.Users.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            solicitudesQuery = solicitudesQuery.Where(solicitud =>
                solicitud.Mensaje.Contains(request.SearchTerm));
        }

        var totalCount = await solicitudesQuery.CountAsync(cancellationToken);

        solicitudesQuery = solicitudesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var solicitudes = await solicitudesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(solicitud => SolicitudViewModel.FromSolicitud(solicitud))
            .ToListAsync(cancellationToken);

        return new PagedResult<SolicitudViewModel>(
            totalCount, solicitudes, request.Query.Page, request.Query.PageSize);
    }
}