using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.GruposInvestigacion;

namespace CleanArchitecture.Application.Interfaces;

public interface IGrupoInvestigacionService
{
    public Task<Guid> CreateGrupoInvestigacionAsync(CreateGrupoInvestigacionViewModel grupoinvestigacion);
    public Task UpdateGrupoInvestigacionAsync(UpdateGrupoInvestigacionViewModel grupoinvestigacion);
    public Task DeleteGrupoInvestigacionAsync(Guid grupoinvestigacionId);
    public Task<GrupoInvestigacionViewModel?> GetGrupoInvestigacionByIdAsync(Guid grupoinvestigacionId);

    public Task<PagedResult<GrupoInvestigacionViewModel>> GetAllGruposInvestigacionAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}