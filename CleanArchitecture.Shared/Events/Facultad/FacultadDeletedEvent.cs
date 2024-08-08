using System;

namespace CleanArchitecture.Shared.Events.Facultad;

public sealed class FacultadDeletedEvent : DomainEvent
{
    public FacultadDeletedEvent(Guid facultadId) : base(facultadId)
    {
    }
}