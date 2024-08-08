using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Calendarios.GetAll;

public sealed class GetAllCalendariosQueryHandler :
    IRequestHandler<GetAllCalendariosQuery, PagedResult<CalendarioViewModel>>
{
    private readonly ISortingExpressionProvider<CalendarioViewModel, Calendario> _sortingExpressionProvider;
    private readonly ICalendarioRepository _calendarioRepository;

    public GetAllCalendariosQueryHandler(
        ICalendarioRepository calendarioRepository,
        ISortingExpressionProvider<CalendarioViewModel, Calendario> sortingExpressionProvider)
    {
        _calendarioRepository = calendarioRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CalendarioViewModel>> Handle(
        GetAllCalendariosQuery request,
        CancellationToken cancellationToken)
    {
        var calendariosQuery = _calendarioRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            calendariosQuery = calendariosQuery.Where(calendario =>
                calendario.AccessToken.Contains(request.SearchTerm));
        }

        var totalCount = await calendariosQuery.CountAsync(cancellationToken);

        calendariosQuery = calendariosQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var calendarios = await calendariosQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(calendario => CalendarioViewModel.FromCalendario(calendario))
            .ToListAsync(cancellationToken);

        return new PagedResult<CalendarioViewModel>(
            totalCount, calendarios, request.Query.Page, request.Query.PageSize);
    }
}