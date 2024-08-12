using System;

namespace CleanArchitecture.Domain.Commands.Contratos.DeleteContrato;

public sealed class DeleteContratosCommand : CommandBase
{
    private static readonly DeleteContratosCommandValidation s_validation = new();

    public DeleteContratosCommand(Guid contratoId) : base(contratoId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}