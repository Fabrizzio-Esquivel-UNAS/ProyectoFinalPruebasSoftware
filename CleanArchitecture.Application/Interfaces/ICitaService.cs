using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Citas;
using CleanArchitecture.Domain.Settings;
using Google.Apis.Calendar.v3.Data;
using System.Threading;

namespace CleanArchitecture.Application.Interfaces;

public interface ICitaService
{
    public Task<Guid> CreateOrUpdateCitaAsync(CreateCitaViewModel cita);
    public Task UpdateCitaAsync(UpdateCitaViewModel cita);
    public Task DeleteCitaAsync(Guid citaId);
    public Task<CitaViewModel?> GetCitaByIdAsync(Guid citaId);

    public Task<PagedResult<CitaViewModel>> GetAllCitasAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}