using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Facultades;
using MediatR;

namespace CleanArchitecture.Application.Queries.Facultades.GetAll;

public sealed record GetAllFacultadesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<FacultadViewModel>>;