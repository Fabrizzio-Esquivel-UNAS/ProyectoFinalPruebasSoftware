using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.LineasInvestigacion.GetAll;
using CleanArchitecture.Application.Queries.LineasInvestigacion.GetLineaInvestigacionById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.LineasInvestigacion.CreateLineaInvestigacion;
using CleanArchitecture.Domain.Commands.LineasInvestigacion.DeleteLineaInvestigacion;
using CleanArchitecture.Domain.Commands.LineasInvestigacion.UpdateLineaInvestigacion;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class LineaInvestigacionService : ILineaInvestigacionService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public LineaInvestigacionService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateLineaInvestigacionAsync(CreateLineaInvestigacionViewModel lineainvestigacion)
    {
        var lineainvestigacionId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateLineaInvestigacionCommand(
            lineainvestigacionId,
            lineainvestigacion.FacultadId,
            lineainvestigacion.GrupoInvestigacionId,
            lineainvestigacion.Nombre));

        return lineainvestigacionId;
    }

    public async Task UpdateLineaInvestigacionAsync(UpdateLineaInvestigacionViewModel lineainvestigacion)
    {
        await _bus.SendCommandAsync(new UpdateLineaInvestigacionCommand(
            lineainvestigacion.Id,
            lineainvestigacion.Nombre));
    }

    public async Task DeleteLineaInvestigacionAsync(Guid lineainvestigacionId)
    {
        await _bus.SendCommandAsync(new DeleteLineaInvestigacionCommand(lineainvestigacionId));
    }

    public async Task<LineaInvestigacionViewModel?> GetLineaInvestigacionByIdAsync(Guid lineainvestigacionId)
    {
        var cachedLineaInvestigacion = await _distributedCache.GetOrCreateJsonAsync(
        CacheKeyGenerator.GetEntityCacheKey<LineaInvestigacion>(lineainvestigacionId),
            async () => await _bus.QueryAsync(new GetLineaInvestigacionByIdQuery(lineainvestigacionId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedLineaInvestigacion;
    }

    public async Task<PagedResult<LineaInvestigacionViewModel>> GetAllLineasInvestigacionAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllLineasInvestigacionQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}