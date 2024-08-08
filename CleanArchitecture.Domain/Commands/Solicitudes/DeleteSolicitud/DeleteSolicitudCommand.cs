using System;

namespace CleanArchitecture.Domain.Commands.Solicitudes.DeleteSolicitud;

public sealed class DeleteSolicitudCommand : CommandBase
{
    private static readonly DeleteSolicitudCommandValidation s_validation = new();

    public DeleteSolicitudCommand(Guid solicitudId) : base(solicitudId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}