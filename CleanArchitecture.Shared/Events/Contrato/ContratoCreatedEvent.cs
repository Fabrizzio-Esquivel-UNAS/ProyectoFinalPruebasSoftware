using System;

namespace CleanArchitecture.Shared.Events.Contrato;

public sealed class ContratoCreatedEvent : DomainEvent
{
    public Guid SolicitudId { get; set; }
    public DateOnly FechaInicio { get; set; }

    public ContratoCreatedEvent(Guid contratoId, Guid solicitudId, DateOnly fechaInicio) : base(contratoId)
    {
        SolicitudId = solicitudId;
        FechaInicio = fechaInicio;
    }
}