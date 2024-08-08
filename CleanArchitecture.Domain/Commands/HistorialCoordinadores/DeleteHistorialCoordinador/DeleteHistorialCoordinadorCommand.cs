using System;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.DeleteHistorialCoordinador;

public sealed class DeleteHistorialCoordinadorCommand : CommandBase
{
    private static readonly DeleteHistorialCoordinadorCommandValidation s_validation = new();

    public DeleteHistorialCoordinadorCommand(Guid historialcoordinadorId) : base(historialcoordinadorId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}