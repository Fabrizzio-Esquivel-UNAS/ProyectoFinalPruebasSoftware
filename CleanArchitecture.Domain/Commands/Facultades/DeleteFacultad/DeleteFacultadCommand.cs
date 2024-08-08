using System;

namespace CleanArchitecture.Domain.Commands.Facultades.DeleteFacultad;

public sealed class DeleteFacultadCommand : CommandBase
{
    private static readonly DeleteFacultadCommandValidation s_validation = new();

    public DeleteFacultadCommand(Guid facultadId) : base(facultadId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}