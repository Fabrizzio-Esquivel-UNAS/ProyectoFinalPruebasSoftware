using System;

namespace CleanArchitecture.Domain.Commands.Contratos.DeleteContrato;

public sealed class DeleteContratoCommand : CommandBase
{
    private static readonly DeleteContratoCommandValidation s_validation = new();

    public DeleteContratoCommand(Guid contratoId) : base(contratoId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}