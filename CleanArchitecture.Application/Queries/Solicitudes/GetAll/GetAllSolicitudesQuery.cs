using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using MediatR;

namespace CleanArchitecture.Application.Queries.Solicitudes.GetAll;

public sealed record GetAllSolicitudesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<SolicitudViewModel>>;