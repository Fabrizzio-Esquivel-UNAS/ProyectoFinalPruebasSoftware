using System;

namespace CleanArchitecture.Shared.Events.Contrato;

public sealed class ContratoDeletedEvent : DomainEvent
{
    public ContratoDeletedEvent(Guid contratoId) : base(contratoId)
    {
    }
}