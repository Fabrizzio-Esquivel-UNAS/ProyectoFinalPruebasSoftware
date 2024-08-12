using System;

namespace CleanArchitecture.Domain.Commands.Escuelas.DeleteEscuela;

public sealed class DeleteEscuelasCommand : CommandBase
{
    private static readonly DeleteEscuelasCommandValidation s_validation = new();

    public DeleteEscuelasCommand(Guid escuelaId) : base(escuelaId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}