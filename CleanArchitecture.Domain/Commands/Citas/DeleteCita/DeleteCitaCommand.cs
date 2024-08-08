using System;

namespace CleanArchitecture.Domain.Commands.Citas.DeleteCita;

public sealed class DeleteCitaCommand : CommandBase
{
    private static readonly DeleteCitaCommandValidation s_validation = new();

    public DeleteCitaCommand(Guid citaId) : base(citaId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}