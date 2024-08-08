using System;

namespace CleanArchitecture.Shared.Events.User;

public sealed class PreferenciasChangedEvent : DomainEvent
{
    public PreferenciasChangedEvent(Guid userId) : base(userId)
    {
    }
}