using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Facultades;

namespace CleanArchitecture.Application.Interfaces;

public interface IFacultadesService
{
    public Task<Guid> CreateFacultadesAsync(CreateFacultadViewModel facultad);
    public Task UpdateFacultadesAsync(UpdateFacultadViewModel facultad);
    public Task DeleteFacultadesAsync(Guid facultadId);
    public Task<FacultadViewModel?> GetFacultadesByIdAsync(Guid facultadId);

    public Task<PagedResult<FacultadViewModel>> GetAllFacultadesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}