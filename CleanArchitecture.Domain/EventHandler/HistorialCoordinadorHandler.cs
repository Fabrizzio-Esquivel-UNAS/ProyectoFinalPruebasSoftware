using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.HistorialCoordinador;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class HistorialCoordinadorEventHandler :
    INotificationHandler<HistorialCoordinadorCreatedEvent>,
    INotificationHandler<HistorialCoordinadorDeletedEvent>,
    INotificationHandler<HistorialCoordinadorUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public HistorialCoordinadorEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(HistorialCoordinadorCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(HistorialCoordinadorDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<HistorialCoordinador>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(HistorialCoordinadorUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<HistorialCoordinador>(notification.AggregateId),
            cancellationToken);
    }
}
