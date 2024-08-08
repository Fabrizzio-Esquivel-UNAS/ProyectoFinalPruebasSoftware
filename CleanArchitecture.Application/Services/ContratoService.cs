using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Contratos.GetAll;
using CleanArchitecture.Application.Queries.Contratos.GetContratoById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Contratos;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Contratos.CreateContrato;
using CleanArchitecture.Domain.Commands.Contratos.DeleteContrato;
using CleanArchitecture.Domain.Commands.Contratos.UpdateContrato;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class ContratoService : IContratoService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public ContratoService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateContratoAsync(CreateContratoViewModel contrato)
    {
        var contratoId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateContratoCommand(
            contratoId,
            contrato.SolicitudId));

        return contratoId;
    }

    public async Task UpdateContratoAsync(UpdateContratoViewModel contrato)
    {
        await _bus.SendCommandAsync(new UpdateContratoCommand(
            contrato.Id));
    }

    public async Task DeleteContratoAsync(Guid contratoId)
    {
        await _bus.SendCommandAsync(new DeleteContratoCommand(contratoId));
    }

    public async Task<ContratoViewModel?> GetContratoByIdAsync(Guid contratoId)
    {
        var cachedContrato = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Contrato>(contratoId),
            async () => await _bus.QueryAsync(new GetContratoByIdQuery(contratoId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedContrato;
    }

    public async Task<PagedResult<ContratoViewModel>> GetAllContratosAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllContratosQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}