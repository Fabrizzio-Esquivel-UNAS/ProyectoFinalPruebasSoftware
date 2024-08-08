using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.DeleteLineaInvestigacion;

public sealed class DeleteLineaInvestigacionCommandValidation : AbstractValidator<DeleteLineaInvestigacionCommand>
{
    public DeleteLineaInvestigacionCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.LineaInvestigacion.EmptyId)
            .WithMessage("LineaInvestigacion id may not be empty");
    }
}