using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Contrato;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class ContratoEventHandler :
    INotificationHandler<ContratoCreatedEvent>,
    INotificationHandler<ContratoDeletedEvent>,
    INotificationHandler<ContratoUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public ContratoEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(ContratoCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(ContratoDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Contrato>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(ContratoUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Contrato>(notification.AggregateId),
            cancellationToken);
    }
}