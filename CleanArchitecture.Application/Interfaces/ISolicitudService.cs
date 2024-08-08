using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using CleanArchitecture.Domain.Settings;
using Google.Apis.Calendar.v3.Data;
using System.Threading;

namespace CleanArchitecture.Application.Interfaces;


public interface ISolicitudService
{
    public Task<Guid> CreateSolicitudAsync(CreateSolicitudViewModel solicitud);
    public Task UpdateSolicitudAsync(UpdateSolicitudViewModel solicitud);
    public Task DeleteSolicitudAsync(Guid solicitudId);
    public Task<SolicitudViewModel?> GetSolicitudByIdAsync(Guid solicitudId);

    public Task<PagedResult<SolicitudViewModel>> GetAllSolicitudesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}