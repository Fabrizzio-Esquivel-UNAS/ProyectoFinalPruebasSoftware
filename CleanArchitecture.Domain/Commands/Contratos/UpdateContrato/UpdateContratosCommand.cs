using System;

namespace CleanArchitecture.Domain.Commands.Contratos.UpdateContrato;

public sealed class UpdateContratosCommand : CommandBase
{
    private static readonly UpdateContratosCommandValidation s_validation = new();

    public Guid ContratoId { get; }

    public UpdateContratosCommand(
        Guid contratoId) : base(contratoId)
    {
        ContratoId = contratoId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}