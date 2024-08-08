using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.LineaInvestigacion;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class LineasInvestigacionEventHandler :
    INotificationHandler<LineaInvestigacionCreatedEvent>,
    INotificationHandler<LineaInvestigacionDeletedEvent>,
    INotificationHandler<LineaInvestigacionUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public LineasInvestigacionEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(LineaInvestigacionCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(LineaInvestigacionDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<LineaInvestigacion>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(LineaInvestigacionUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<LineaInvestigacion>(notification.AggregateId),
            cancellationToken);
    }
}