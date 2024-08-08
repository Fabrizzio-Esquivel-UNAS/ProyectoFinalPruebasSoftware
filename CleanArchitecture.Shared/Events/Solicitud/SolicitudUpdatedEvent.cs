using System;

namespace CleanArchitecture.Shared.Events.Solicitud;

public sealed class SolicitudUpdatedEvent : DomainEvent
{
    public int Estado { get; set; }

    public SolicitudUpdatedEvent(
        Guid solicitudId, 
        int estado) : base(solicitudId)
    {
        Estado = estado;
    }
}