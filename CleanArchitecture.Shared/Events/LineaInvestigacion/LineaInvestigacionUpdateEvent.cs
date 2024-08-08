using System;

namespace CleanArchitecture.Shared.Events.LineaInvestigacion;

public sealed class LineaInvestigacionUpdatedEvent : DomainEvent
{
    public string Nombre { get; set; }

    public LineaInvestigacionUpdatedEvent(Guid lineainvestigacionId, string nombre) : base(lineainvestigacionId)
    {
        Nombre = nombre;
    }
}