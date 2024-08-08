using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Escuelas.CreateEscuela;

public sealed class CreateEscuelaCommandValidation : AbstractValidator<CreateEscuelaCommand>
{
    public CreateEscuelaCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Escuela.EmptyId)
            .WithMessage("Escuela id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Nombre)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Escuela.EmptyNombre)
            .WithMessage("Nombre may not be empty")
            .MaximumLength(MaxLengths.Escuela.Nombre)
            .WithErrorCode(DomainErrorCodes.Escuela.NombreExceedsMaxLength)
            .WithMessage($"Nombre may not be longer than {MaxLengths.Escuela.Nombre} characters");
    }
}