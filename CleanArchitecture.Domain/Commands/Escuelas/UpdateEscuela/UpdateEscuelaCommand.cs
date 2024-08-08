using System;

namespace CleanArchitecture.Domain.Commands.Escuelas.UpdateEscuela;

public sealed class UpdateEscuelaCommand : CommandBase
{
    private static readonly UpdateEscuelaCommandValidation s_validation = new();

    public string Nombre { get; }

    public UpdateEscuelaCommand(Guid escuelaId, string nombre) : base(escuelaId)
    {
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}