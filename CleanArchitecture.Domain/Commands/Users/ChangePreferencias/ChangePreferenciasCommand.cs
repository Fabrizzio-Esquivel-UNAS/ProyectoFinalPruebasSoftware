using System;

namespace CleanArchitecture.Domain.Commands.Users.ChangePreferencias;

public sealed class ChangePreferenciasCommand : CommandBase
{
    private static readonly ChangePreferenciasCommandValidation s_validation = new();

    public string NewPreferencias { get; }

    public ChangePreferenciasCommand(string newPreferencias) : base(Guid.NewGuid())
    {
        NewPreferencias = newPreferencias;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}