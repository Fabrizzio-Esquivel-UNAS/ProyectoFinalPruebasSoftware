using System;

namespace CleanArchitecture.Domain.Commands.Contratos.UpdateContrato;

public sealed class UpdateContratoCommand : CommandBase
{
    private static readonly UpdateContratoCommandValidation s_validation = new();

    public Guid ContratoId { get; }

    public UpdateContratoCommand(
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