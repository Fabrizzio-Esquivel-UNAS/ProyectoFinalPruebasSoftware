using System;

namespace CleanArchitecture.Domain.Commands.Escuelas.CreateEscuela;

public sealed class CreateEscuelaCommand : CommandBase
{
    private static readonly CreateEscuelaCommandValidation s_validation = new();

    public Guid FacultadId { get; }
    public string Nombre { get; }

    public CreateEscuelaCommand(
        Guid escuelaId,
        Guid facultadId,
        string nombre) : base(escuelaId)
    {
        FacultadId = facultadId;
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}