using System;

namespace CleanArchitecture.Shared.Events.Solicitud;

public sealed class SolicitudCreatedEvent : DomainEvent
{
    public Guid AsesoradoUserId { get; set; }
    public Guid AsesorUserId { get; set; }
    public int NumeroTesis { get; set; }

    public string Mensaje { get; set; }

    public SolicitudCreatedEvent(Guid solicitudId, Guid asesoradoUserId,
        Guid asesorUserId,
        int numeroTesis, string mensaje) : base(solicitudId)
    {
        AsesoradoUserId = asesoradoUserId;
        AsesorUserId = asesorUserId;
        NumeroTesis = numeroTesis;
        Mensaje = mensaje;
    }
}