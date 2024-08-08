using System;

namespace CleanArchitecture.Shared.Events.Solicitud;

public sealed class SolicitudDeletedEvent : DomainEvent
{
    public SolicitudDeletedEvent(Guid solicitudId) : base(solicitudId)
    {
    }
}