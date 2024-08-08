using System;

namespace CleanArchitecture.Shared.Events.Escuela;

public sealed class EscuelaUpdatedEvent : DomainEvent
{
    public string Nombre { get; set; }

    public EscuelaUpdatedEvent(Guid escuelaId, string nombre) : base(escuelaId)
    {
        Nombre = nombre;
    }
}