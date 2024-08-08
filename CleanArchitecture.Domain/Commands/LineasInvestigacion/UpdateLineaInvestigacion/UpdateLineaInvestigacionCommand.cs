using System;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.UpdateLineaInvestigacion;

public sealed class UpdateLineaInvestigacionCommand : CommandBase
{
    private static readonly UpdateLineaInvestigacionCommandValidation s_validation = new();

    public string Nombre { get; }

    public UpdateLineaInvestigacionCommand(Guid lineainvestigacionId, string nombre) : base(lineainvestigacionId)
    {
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}