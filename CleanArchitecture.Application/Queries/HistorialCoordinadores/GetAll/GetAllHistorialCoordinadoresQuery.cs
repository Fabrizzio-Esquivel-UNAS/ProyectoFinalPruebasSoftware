using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;
using MediatR;

namespace CleanArchitecture.Application.Queries.HistorialCoordinadores.GetAll;

public sealed record GetAllHistorialCoordinadoresQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<HistorialCoordinadorViewModel>>;