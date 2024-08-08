using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;

namespace CleanArchitecture.Application.Interfaces;

public interface IHistorialCoordinadorService
{
    public Task<Guid> CreateHistorialCoordinadorAsync(CreateHistorialCoordinadorViewModel historialcoordinador);
    public Task UpdateHistorialCoordinadorAsync(UpdateHistorialCoordinadorViewModel historialcoordinador);
    public Task DeleteHistorialCoordinadorAsync(Guid historialcoordinadorId);
    public Task<HistorialCoordinadorViewModel?> GetHistorialCoordinadorByIdAsync(Guid historialcoordinadorId);

    public Task<PagedResult<HistorialCoordinadorViewModel>> GetAllHistorialCoordinadoresAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}