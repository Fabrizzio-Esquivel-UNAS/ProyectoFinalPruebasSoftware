using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.CreateLineaInvestigacion;

public sealed class CreateLineaInvestigacionCommandValidation : AbstractValidator<CreateLineaInvestigacionCommand>
{
    public CreateLineaInvestigacionCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.LineaInvestigacion.EmptyId)
            .WithMessage("LineaInvestigacion id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Nombre)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.LineaInvestigacion.EmptyNombre)
            .WithMessage("Nombre may not be empty")
            .MaximumLength(MaxLengths.LineaInvestigacion.Nombre)
            .WithErrorCode(DomainErrorCodes.LineaInvestigacion.NombreExceedsMaxLength)
            .WithMessage($"Nombre may not be longer than {MaxLengths.LineaInvestigacion.Nombre} characters");
    }
}