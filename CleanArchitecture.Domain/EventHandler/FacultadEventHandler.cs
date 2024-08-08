using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Facultad;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class FacultadesEventHandler :
    INotificationHandler<FacultadCreatedEvent>,
    INotificationHandler<FacultadDeletedEvent>,
    INotificationHandler<FacultadUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public FacultadesEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(FacultadCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(FacultadDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Facultad>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(FacultadUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Facultad>(notification.AggregateId),
            cancellationToken);
    }
}