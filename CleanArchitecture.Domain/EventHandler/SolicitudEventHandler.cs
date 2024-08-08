using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Solicitud;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class SolicitudEventHandler :
    INotificationHandler<SolicitudCreatedEvent>,
    INotificationHandler<SolicitudDeletedEvent>,
    INotificationHandler<SolicitudUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public SolicitudEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(SolicitudCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(SolicitudDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Solicitud>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(SolicitudUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Solicitud>(notification.AggregateId),
            cancellationToken);
    }
}