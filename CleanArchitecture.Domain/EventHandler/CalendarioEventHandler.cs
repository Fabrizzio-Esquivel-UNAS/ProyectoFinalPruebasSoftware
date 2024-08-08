using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Calendario;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class CalendarioEventHandler :
    INotificationHandler<CalendarioCreatedEvent>,
    INotificationHandler<CalendarioDeletedEvent>,
    INotificationHandler<CalendarioUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public CalendarioEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(CalendarioCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(CalendarioDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Calendario>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(CalendarioUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Calendario>(notification.AggregateId),
            cancellationToken);
    }
}