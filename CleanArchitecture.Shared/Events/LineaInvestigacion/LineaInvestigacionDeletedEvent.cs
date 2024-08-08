using System;

namespace CleanArchitecture.Shared.Events.LineaInvestigacion;

public sealed class LineaInvestigacionDeletedEvent : DomainEvent
{
    public LineaInvestigacionDeletedEvent(Guid lineainvestigacionId) : base(lineainvestigacionId)
    {
    }
}