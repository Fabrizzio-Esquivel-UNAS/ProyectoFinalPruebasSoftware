using System;

namespace CleanArchitecture.Domain.Commands.Contratos.CreateContrato;

public sealed class CreateContratoCommand : CommandBase
{
    private static readonly CreateContratoCommandValidation s_validation = new();

    public Guid ContratoId { get; }
    public Guid SolicitudId { get; }

    public CreateContratoCommand(
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