using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Facultades.CreateFacultad;

public sealed class CreateFacultadesCommandValidation : AbstractValidator<CreateFacultadesCommand>
{
    public CreateFacultadesCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Facultad.EmptyId)
            .WithMessage("Facultad id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Nombre)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Facultad.EmptyNombre)
            .WithMessage("Nombre may not be empty")
            .MaximumLength(MaxLengths.Facultad.Nombre)
            .WithErrorCode(DomainErrorCodes.Facultad.NombreExceedsMaxLength)
            .WithMessage($"Nombre may not be longer than {MaxLengths.Facultad.Nombre} characters");
    }
}