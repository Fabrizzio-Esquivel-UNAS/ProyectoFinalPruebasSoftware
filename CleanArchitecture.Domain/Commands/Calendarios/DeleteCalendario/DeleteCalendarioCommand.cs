using System;

namespace CleanArchitecture.Domain.Commands.Calendarios.DeleteCalendario;

public sealed class DeleteCalendarioCommand : CommandBase
{
    private static readonly DeleteCalendarioCommandValidation s_validation = new();

    public DeleteCalendarioCommand(Guid calendarioId) : base(calendarioId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}