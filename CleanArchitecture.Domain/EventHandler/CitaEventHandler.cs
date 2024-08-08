using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Cita;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class CitaEventHandler :
    INotificationHandler<CitaCreatedEvent>,
    INotificationHandler<CitaDeletedEvent>,
    INotificationHandler<CitaUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public CitaEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(CitaCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(CitaDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Cita>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(CitaUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Cita>(notification.AggregateId),
            cancellationToken);
    }
}