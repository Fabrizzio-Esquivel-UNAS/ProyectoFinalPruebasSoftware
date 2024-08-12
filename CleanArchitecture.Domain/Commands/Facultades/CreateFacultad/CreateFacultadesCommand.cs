using System;

namespace CleanArchitecture.Domain.Commands.Facultades.CreateFacultad;

public sealed class CreateFacultadesCommand : CommandBase
{
    private static readonly CreateFacultadesCommandValidation s_validation = new();

    public string Nombre { get; }

    public CreateFacultadesCommand(Guid facultadId, string nombre) : base(facultadId)
    {
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}