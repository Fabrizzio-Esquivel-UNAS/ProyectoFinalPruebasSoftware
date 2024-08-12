using System;

namespace CleanArchitecture.Domain.Commands.Facultades.DeleteFacultad;

public sealed class DeleteFacultadesCommand : CommandBase
{
    private static readonly DeleteFacultadesCommandValidation s_validation = new();

    public DeleteFacultadesCommand(Guid facultadId) : base(facultadId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}