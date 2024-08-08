using System;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;

public sealed class UpdateHistorialCoordinadorCommand : CommandBase
{
    private static readonly UpdateHistorialCoordinadorCommandValidation s_validation = new();

    public Guid HistorialCoordinadorId { get; }
    public DateTime FechaFin { get; }

    public UpdateHistorialCoordinadorCommand(
        Guid historialCoordinadorId,
        DateTime fechafin) : base(historialCoordinadorId)
    {
        HistorialCoordinadorId = historialCoordinadorId;
        FechaFin = fechafin;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}