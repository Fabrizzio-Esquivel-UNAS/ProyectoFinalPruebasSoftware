using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Escuelas.GetAll;
using CleanArchitecture.Application.Queries.Escuelas.GetEscuelasById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Escuelas;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Escuelas.CreateEscuela;
using CleanArchitecture.Domain.Commands.Escuelas.DeleteEscuela;
using CleanArchitecture.Domain.Commands.Escuelas.UpdateEscuela;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class EscuelasService : IEscuelasService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public EscuelasService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateEscuelasAsync(CreateEscuelaViewModel escuela)
    {
        var escuelaId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateEscuelasCommand(
            escuelaId,
            escuela.FacultadId,
            escuela.Nombre));

        return escuelaId;
    }

    public async Task UpdateEscuelasAsync(UpdateEscuelaViewModel escuela)
    {
        await _bus.SendCommandAsync(new UpdateEscuelasCommand(
            escuela.Id,
            escuela.Nombre));
    }

    public async Task DeleteEscuelasAsync(Guid escuelaId)
    {
        await _bus.SendCommandAsync(new DeleteEscuelasCommand(escuelaId));
    }

    public async Task<EscuelaViewModel?> GetEscuelasByIdAsync(Guid escuelaId)
    {
        var cachedEscuelas = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Escuela>(escuelaId),
            async () => await _bus.QueryAsync(new GetEscuelasByIdQuery(escuelaId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedEscuelas;
    }

    public async Task<PagedResult<EscuelaViewModel>> GetAllEscuelasAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllEscuelasQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}