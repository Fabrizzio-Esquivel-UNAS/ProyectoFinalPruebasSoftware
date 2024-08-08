using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Calendarios;
using MediatR;

namespace CleanArchitecture.Application.Queries.Calendarios.GetAll;

public sealed record GetAllCalendariosQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<CalendarioViewModel>>;