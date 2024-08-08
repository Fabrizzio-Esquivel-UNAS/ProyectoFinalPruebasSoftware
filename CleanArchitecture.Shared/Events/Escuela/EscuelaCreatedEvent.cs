using System;

namespace CleanArchitecture.Shared.Events.Escuela;

public sealed class EscuelaCreatedEvent : DomainEvent
{
    public string Nombre { get; set; }

    public EscuelaCreatedEvent(Guid escuelaId, string nombre) : base(escuelaId)
    {
        Nombre = nombre;
    }
}