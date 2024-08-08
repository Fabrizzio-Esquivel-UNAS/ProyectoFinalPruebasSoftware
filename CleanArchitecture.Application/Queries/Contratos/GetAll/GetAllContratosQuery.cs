using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Contratos;
using MediatR;

namespace CleanArchitecture.Application.Queries.Contratos.GetAll;

public sealed record GetAllContratosQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<ContratoViewModel>>;