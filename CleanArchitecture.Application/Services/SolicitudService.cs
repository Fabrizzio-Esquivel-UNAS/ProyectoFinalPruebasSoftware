using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Solicitudes.GetAll;
using CleanArchitecture.Application.Queries.Solicitudes.GetSolicitudById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;
using CleanArchitecture.Domain.Commands.Solicitudes.DeleteSolicitud;
using CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Commands.Contratos.CreateContrato;

namespace CleanArchitecture.Application.Services;

public sealed class SolicitudService : ISolicitudService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public SolicitudService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateSolicitudAsync(CreateSolicitudViewModel solicitud)
    {
        var solicitudId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateSolicitudCommand(
            solicitudId,
            solicitud.AsesoradoUserId,
            solicitud.AsesorUserId,
            solicitud.NumeroTesis,
            solicitud.Mensaje));

        return solicitudId;
    }

    public async Task UpdateSolicitudAsync(UpdateSolicitudViewModel solicitud)
    {
        await _bus.SendCommandAsync(new UpdateSolicitudCommand(
            solicitud.Id,
            solicitud.Estado));
    }

    public async Task DeleteSolicitudAsync(Guid solicitudId)
    {
        await _bus.SendCommandAsync(new DeleteSolicitudCommand(solicitudId));
    }

    public async Task<SolicitudViewModel?> GetSolicitudByIdAsync(Guid solicitudId)
    {
        var cachedSolicitud = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Solicitud>(solicitudId),
            async () => await _bus.QueryAsync(new GetSolicitudByIdQuery(solicitudId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedSolicitud;
    }

    public async Task<PagedResult<SolicitudViewModel>> GetAllSolicitudesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllSolicitudesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}