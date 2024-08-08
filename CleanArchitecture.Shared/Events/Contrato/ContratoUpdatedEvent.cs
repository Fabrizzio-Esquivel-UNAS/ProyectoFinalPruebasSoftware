using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Shared.Events.Contrato;

public sealed class ContratoUpdatedEvent : DomainEvent
{
    public Guid SolicitudId { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFinal { get; set; }

    public ContratoUpdatedEvent(Guid contratoId, Guid solicitudId, DateOnly fechaInicio, DateOnly fechaFinal) : base(contratoId)
    {
        SolicitudId = solicitudId;
        FechaInicio = fechaInicio;
        FechaFinal = fechaFinal;
    }
}