using System;

namespace CleanArchitecture.Shared.Events.LineaInvestigacion;

public sealed class LineaInvestigacionCreatedEvent : DomainEvent
{
    public string Nombre { get; set; }

    public LineaInvestigacionCreatedEvent(Guid lineainvestigacionId, string nombre) : base(lineainvestigacionId)
    {
        Nombre = nombre;
    }
}