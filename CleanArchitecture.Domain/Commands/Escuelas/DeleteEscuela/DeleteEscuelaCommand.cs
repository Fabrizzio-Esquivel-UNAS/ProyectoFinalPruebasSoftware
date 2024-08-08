using System;

namespace CleanArchitecture.Domain.Commands.Escuelas.DeleteEscuela;

public sealed class DeleteEscuelaCommand : CommandBase
{
    private static readonly DeleteEscuelaCommandValidation s_validation = new();

    public DeleteEscuelaCommand(Guid escuelaId) : base(escuelaId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}