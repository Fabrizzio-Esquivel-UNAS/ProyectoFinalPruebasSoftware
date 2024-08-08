using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.HistorialCoordinadores.GetAll;
using CleanArchitecture.Application.Queries.HistorialCoordinadores.GetHistorialCoordinadorById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.DeleteHistorialCoordinador;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class HistorialCoordinadorService : IHistorialCoordinadorService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public HistorialCoordinadorService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateHistorialCoordinadorAsync(CreateHistorialCoordinadorViewModel historialcoordinador)
    {
        var historialcoordinadorId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateHistorialCoordinadorCommand(
            historialcoordinadorId,
            historialcoordinador.UserId,
            historialcoordinador.GrupoInvestigacionId,
            historialcoordinador.FechaInicio));

        return historialcoordinadorId;
    }

    public async Task UpdateHistorialCoordinadorAsync(UpdateHistorialCoordinadorViewModel historialcoordinador)
    {
        await _bus.SendCommandAsync(new UpdateHistorialCoordinadorCommand(
            historialcoordinador.Id,
            historialcoordinador.FechaFin));
    }

    public async Task DeleteHistorialCoordinadorAsync(Guid historialcoordinadorId)
    {
        await _bus.SendCommandAsync(new DeleteHistorialCoordinadorCommand(historialcoordinadorId));
    }

    public async Task<HistorialCoordinadorViewModel?> GetHistorialCoordinadorByIdAsync(Guid historialcoordinadorId)
    {
        var cachedHistorialCoordinador = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<HistorialCoordinador>(historialcoordinadorId),
            async () => await _bus.QueryAsync(new GetHistorialCoordinadorByIdQuery(historialcoordinadorId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedHistorialCoordinador;
    }

    public async Task<PagedResult<HistorialCoordinadorViewModel>> GetAllHistorialCoordinadoresAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllHistorialCoordinadoresQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}