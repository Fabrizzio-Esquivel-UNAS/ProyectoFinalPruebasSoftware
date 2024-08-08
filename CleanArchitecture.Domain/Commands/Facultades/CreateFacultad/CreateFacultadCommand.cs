using System;

namespace CleanArchitecture.Domain.Commands.Facultades.CreateFacultad;

public sealed class CreateFacultadCommand : CommandBase
{
    private static readonly CreateFacultadCommandValidation s_validation = new();

    public string Nombre { get; }

    public CreateFacultadCommand(Guid facultadId, string nombre) : base(facultadId)
    {
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}