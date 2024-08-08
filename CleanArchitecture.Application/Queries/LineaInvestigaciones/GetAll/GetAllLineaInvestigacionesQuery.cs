using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;
using MediatR;

namespace CleanArchitecture.Application.Queries.LineasInvestigacion.GetAll;

public sealed record GetAllLineasInvestigacionQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<LineaInvestigacionViewModel>>;