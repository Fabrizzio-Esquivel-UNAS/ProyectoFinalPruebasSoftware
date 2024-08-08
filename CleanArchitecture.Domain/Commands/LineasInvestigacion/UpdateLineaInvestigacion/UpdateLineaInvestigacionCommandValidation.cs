using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.UpdateLineaInvestigacion;

public sealed class UpdateLineaInvestigacionCommandValidation : AbstractValidator<UpdateLineaInvestigacionCommand>
{
    public UpdateLineaInvestigacionCommandValidation()
    {
        AddRuleForId();
        AddRuleForNombre();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.LineaInvestigacion.EmptyId)
            .WithMessage("LineaInvestigacion id may not be empty");
    }

    private void AddRuleForNombre()
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