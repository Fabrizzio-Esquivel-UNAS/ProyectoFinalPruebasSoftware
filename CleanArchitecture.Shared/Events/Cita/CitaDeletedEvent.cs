using System;

namespace CleanArchitecture.Shared.Events.Cita;

public sealed class CitaDeletedEvent : DomainEvent
{
    public string EventoId { get; set; }
    public Guid AsesorUserId { get; set; }
    public Guid AsesoradoUserId { get; set; }

    public CitaDeletedEvent(Guid citaId, string eventoId, Guid asesorUserId, Guid asesoradoUserId) : base(citaId)
    {
        EventoId = eventoId;
        AsesorUserId = asesorUserId;
        AsesoradoUserId = asesoradoUserId;
    }
}