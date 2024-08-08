using CleanArchitecture.Shared.Events.GrupoInvestigacion;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;


namespace CleanArchitecture.Domain.EventHandler;
public sealed class GrupoInvestigacionEventHandler :
    INotificationHandler<GrupoInvestigacionCreatedEvent>,
    INotificationHandler<GrupoInvestigacionDeletedEvent>,
    INotificationHandler<GrupoInvestigacionUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public GrupoInvestigacionEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(GrupoInvestigacionCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(GrupoInvestigacionDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<GrupoInvestigacion>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(GrupoInvestigacionUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<GrupoInvestigacion>(notification.AggregateId),
            cancellationToken);
    }
}
