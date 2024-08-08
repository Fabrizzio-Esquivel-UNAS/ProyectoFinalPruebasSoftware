using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;

namespace CleanArchitecture.Application.Interfaces;

public interface ILineaInvestigacionService
{
    public Task<Guid> CreateLineaInvestigacionAsync(CreateLineaInvestigacionViewModel lineainvestigacion);
    public Task UpdateLineaInvestigacionAsync(UpdateLineaInvestigacionViewModel lineainvestigacion);
    public Task DeleteLineaInvestigacionAsync(Guid lineainvestigacionId);
    public Task<LineaInvestigacionViewModel?> GetLineaInvestigacionByIdAsync(Guid lineainvestigacionId);

    public Task<PagedResult<LineaInvestigacionViewModel>> GetAllLineasInvestigacionAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}