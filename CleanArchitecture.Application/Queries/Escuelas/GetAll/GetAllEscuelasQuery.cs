using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Escuelas;
using MediatR;

namespace CleanArchitecture.Application.Queries.Escuelas.GetAll;

public sealed record GetAllEscuelasQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<EscuelaViewModel>>;