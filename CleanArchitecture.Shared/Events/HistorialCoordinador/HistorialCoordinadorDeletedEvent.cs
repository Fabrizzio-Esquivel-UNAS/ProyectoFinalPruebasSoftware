using System;

namespace CleanArchitecture.Shared.Events.HistorialCoordinador;

public sealed class HistorialCoordinadorDeletedEvent : DomainEvent
{
    public HistorialCoordinadorDeletedEvent(Guid historialcoordinadorId) : base(historialcoordinadorId)
    {
    }
}