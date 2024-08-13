using System;

namespace CleanArchitecture.Domain.Commands.Contratos.CreateContrato;

public sealed class CreateContratosCommand : CommandBase
{
    private static readonly CreateContratosCommandValidation s_validation = new();

    public Guid ContratoId { get; }
    public Guid SolicitudId { get; }

    public CreateContratosCommand(
        Guid contratoId,
        Guid solicitudId) : base(contratoId)
    {
        ContratoId = contratoId;
        SolicitudId = solicitudId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}