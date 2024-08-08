using System;

namespace CleanArchitecture.Shared.Events.Facultad;

public sealed class FacultadUpdatedEvent : DomainEvent
{
    public string Nombre { get; set; }

    public FacultadUpdatedEvent(Guid facultadId, string nombre) : base(facultadId)
    {
        Nombre = nombre;
    }
}