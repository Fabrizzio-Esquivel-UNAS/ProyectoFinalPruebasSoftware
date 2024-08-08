using System;

namespace CleanArchitecture.Shared.Events.Facultad;

public sealed class FacultadCreatedEvent : DomainEvent
{
    public string Nombre { get; set; }

    public FacultadCreatedEvent(Guid facultadId, string nombre) : base(facultadId)
    {
        Nombre = nombre;
    }
}