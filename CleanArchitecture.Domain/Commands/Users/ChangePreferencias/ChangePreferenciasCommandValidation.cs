using CleanArchitecture.Domain.Extensions.Validation;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Users.ChangePreferencias;

public sealed class ChangePreferenciasCommandValidation : AbstractValidator<ChangePreferenciasCommand>
{
    public ChangePreferenciasCommandValidation()
    {
    }
}