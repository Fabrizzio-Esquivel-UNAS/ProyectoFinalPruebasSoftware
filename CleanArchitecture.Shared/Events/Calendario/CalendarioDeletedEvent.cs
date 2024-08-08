using System;

namespace CleanArchitecture.Shared.Events.Calendario;

public sealed class CalendarioDeletedEvent : DomainEvent
{
    public CalendarioDeletedEvent(Guid calendarioId) : base(calendarioId)
    {
    }
}