using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.GruposInvestigacion;
using MediatR;

namespace CleanArchitecture.Application.Queries.GruposInvestigacion.GetAll;

public sealed record GetAllGruposInvestigacionQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<GrupoInvestigacionViewModel>>;