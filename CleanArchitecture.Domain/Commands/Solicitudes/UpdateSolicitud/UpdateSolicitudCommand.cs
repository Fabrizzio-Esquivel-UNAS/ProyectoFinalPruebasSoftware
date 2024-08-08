using System;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;

public sealed class UpdateSolicitudCommand : CommandBase
{
    private static readonly UpdateSolicitudCommandValidation s_validation = new();

    public SolicitudStatus Estado { get; }

    public UpdateSolicitudCommand(Guid solicitudId, SolicitudStatus estado) : base(solicitudId)
    {
        Estado = estado;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}