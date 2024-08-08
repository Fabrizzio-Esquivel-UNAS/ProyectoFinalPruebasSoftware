using System;

namespace CleanArchitecture.Domain.Commands.Facultades.UpdateFacultad;

public sealed class UpdateFacultadCommand : CommandBase
{
    private static readonly UpdateFacultadCommandValidation s_validation = new();

    public string Nombre { get; }

    public UpdateFacultadCommand(Guid facultadId, string nombre) : base(facultadId)
    {
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}