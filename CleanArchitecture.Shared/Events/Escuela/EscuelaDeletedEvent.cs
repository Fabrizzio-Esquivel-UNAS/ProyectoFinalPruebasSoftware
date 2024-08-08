using System;

namespace CleanArchitecture.Shared.Events.Escuela;

public sealed class EscuelaDeletedEvent : DomainEvent
{
    public EscuelaDeletedEvent(Guid escuelaId) : base(escuelaId)
    {
    }
}