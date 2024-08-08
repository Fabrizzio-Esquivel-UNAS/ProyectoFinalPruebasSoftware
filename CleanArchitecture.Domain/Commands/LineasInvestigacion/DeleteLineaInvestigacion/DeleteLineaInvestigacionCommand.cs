using System;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.DeleteLineaInvestigacion;

public sealed class DeleteLineaInvestigacionCommand : CommandBase
{
    private static readonly DeleteLineaInvestigacionCommandValidation s_validation = new();

    public DeleteLineaInvestigacionCommand(Guid lineainvestigacionId) : base(lineainvestigacionId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}