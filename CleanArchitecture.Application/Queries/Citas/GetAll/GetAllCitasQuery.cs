using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Citas;
using MediatR;

namespace CleanArchitecture.Application.Queries.Citas.GetAll;

public sealed record GetAllCitasQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<CitaViewModel>>;