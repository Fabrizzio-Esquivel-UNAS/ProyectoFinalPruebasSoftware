using System;

namespace CleanArchitecture.Domain.Commands.Escuelas.CreateEscuela;

public sealed class CreateEscuelasCommand : CommandBase
{
    private static readonly CreateEscuelasCommandValidation s_validation = new();

    public Guid FacultadId { get; }
    public string Nombre { get; }

    public CreateEscuelasCommand(
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