using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Facultades.GetAll;
using CleanArchitecture.Application.Queries.Facultades.GetFacultadesById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Facultades;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Facultades.CreateFacultad;
using CleanArchitecture.Domain.Commands.Facultades.DeleteFacultad;
using CleanArchitecture.Domain.Commands.Facultades.UpdateFacultad;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class FacultadesService : IFacultadesService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public FacultadesService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateFacultadesAsync(CreateFacultadViewModel facultad)
    {
        var facultadId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateFacultadCommand(
            facultadId,
            facultad.Nombre));

        return facultadId;
    }

    public async Task UpdateFacultadesAsync(UpdateFacultadViewModel facultad)
    {
        await _bus.SendCommandAsync(new UpdateFacultadCommand(
            facultad.Id,
            facultad.Nombre));
    }

    public async Task DeleteFacultadesAsync(Guid facultadId)
    {
        await _bus.SendCommandAsync(new DeleteFacultadCommand(facultadId));
    }

    public async Task<FacultadViewModel?> GetFacultadesByIdAsync(Guid facultadId)
    {
        var cachedFacultades = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Facultad>(facultadId),
            async () => await _bus.QueryAsync(new GetFacultadesByIdQuery(facultadId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedFacultades;
    }

    public async Task<PagedResult<FacultadViewModel>> GetAllFacultadesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllFacultadesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}