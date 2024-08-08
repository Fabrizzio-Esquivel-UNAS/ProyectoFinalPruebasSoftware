using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Escuelas;

namespace CleanArchitecture.Application.Interfaces;

public interface IEscuelasService
{
    public Task<Guid> CreateEscuelasAsync(CreateEscuelaViewModel escuela);
    public Task UpdateEscuelasAsync(UpdateEscuelaViewModel escuela);
    public Task DeleteEscuelasAsync(Guid escuelaId);
    public Task<EscuelaViewModel?> GetEscuelasByIdAsync(Guid escuelaId);

    public Task<PagedResult<EscuelaViewModel>> GetAllEscuelasAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}