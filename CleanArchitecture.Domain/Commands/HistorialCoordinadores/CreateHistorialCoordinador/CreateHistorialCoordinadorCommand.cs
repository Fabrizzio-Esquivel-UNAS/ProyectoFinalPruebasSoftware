using System;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;

public sealed class CreateHistorialCoordinadorCommand : CommandBase
{
    private static readonly CreateHistorialCoordinadorCommandValidation s_validation = new();

    public Guid HistorialCoordinadorId { get; }
    public Guid UserId { get; private set; }

    public Guid GrupoInvestigacionId { get; private set; }
    public DateTime FechaInicio { get; private set; }

    public CreateHistorialCoordinadorCommand(
        Guid historialcoordinadorId,
        Guid userId,
        Guid grupoInvestigacionId,
        DateTime fechaInicio) : base(historialcoordinadorId)
    {
        HistorialCoordinadorId = historialcoordinadorId;
        UserId = userId;
        GrupoInvestigacionId = grupoInvestigacionId;
        FechaInicio = fechaInicio;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}