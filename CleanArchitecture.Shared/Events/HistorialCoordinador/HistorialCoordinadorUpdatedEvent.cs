using System;

namespace CleanArchitecture.Shared.Events.HistorialCoordinador;

public sealed class HistorialCoordinadorUpdatedEvent : DomainEvent
{

    public Guid UserId { get; set; }
    public Guid GrupoInvestigacionId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public HistorialCoordinadorUpdatedEvent(Guid historialcoordinadorId, Guid userId,
        Guid grupoinvestigacionId,
        DateTime fechainicio,
        DateTime? fechafin) : base(historialcoordinadorId)
    {
        UserId = userId;
        GrupoInvestigacionId = grupoinvestigacionId;
        FechaInicio = fechainicio;
        FechaFin = fechafin;
    }
}