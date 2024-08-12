using System;

namespace CleanArchitecture.Domain.Commands.Facultades.UpdateFacultad;

public sealed class UpdateFacultadesCommand : CommandBase
{
    private static readonly UpdateFacultadesCommandValidation s_validation = new();

    public string Nombre { get; }

    public UpdateFacultadesCommand(Guid facultadId, string nombre) : base(facultadId)
    {
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}