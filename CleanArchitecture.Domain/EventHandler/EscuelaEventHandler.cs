using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Escuela;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class EscuelasEventHandler :
    INotificationHandler<EscuelaCreatedEvent>,
    INotificationHandler<EscuelaDeletedEvent>,
    INotificationHandler<EscuelaUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public EscuelasEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(EscuelaCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(EscuelaDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Escuela>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(EscuelaUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Escuela>(notification.AggregateId),
            cancellationToken);
    }
}