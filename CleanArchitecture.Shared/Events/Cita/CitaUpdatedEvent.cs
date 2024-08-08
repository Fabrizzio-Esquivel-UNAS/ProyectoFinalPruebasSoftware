using System;

namespace CleanArchitecture.Shared.Events.Cita;

public sealed class CitaUpdatedEvent : DomainEvent
{
    public string EventoId { get; set; }
    public Guid AsesorUserId { get; set; }
    public Guid AsesoradoUserId { get; set; }

    public CitaUpdatedEvent(Guid citaId, string eventoId, Guid asesorUserId, Guid asesoradoUserId) : base(citaId)
    {
        EventoId = eventoId;
        AsesorUserId = asesorUserId;
        AsesoradoUserId = asesoradoUserId;
    }
}