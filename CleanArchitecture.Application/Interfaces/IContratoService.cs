using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Contratos;

namespace CleanArchitecture.Application.Interfaces;

public interface IContratoService
{
    public Task<Guid> CreateContratoAsync(CreateContratoViewModel contrato);
    public Task UpdateContratoAsync(UpdateContratoViewModel contrato);
    public Task DeleteContratoAsync(Guid contratoId);
    public Task<ContratoViewModel?> GetContratoByIdAsync(Guid contratoId);

    public Task<PagedResult<ContratoViewModel>> GetAllContratosAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}