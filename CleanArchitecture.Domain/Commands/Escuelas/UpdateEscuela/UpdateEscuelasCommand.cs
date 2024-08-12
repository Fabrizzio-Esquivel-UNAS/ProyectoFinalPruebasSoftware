using System;

namespace CleanArchitecture.Domain.Commands.Escuelas.UpdateEscuela;

public sealed class UpdateEscuelasCommand : CommandBase
{
    private static readonly UpdateEscuelasCommandValidation s_validation = new();

    public string Nombre { get; }

    public UpdateEscuelasCommand(Guid escuelaId, string nombre) : base(escuelaId)
    {
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}