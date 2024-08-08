using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Calendarios.UpdateCalendario;

public sealed class UpdateCalendarioCommandValidation : AbstractValidator<UpdateCalendarioCommand>
{
    public UpdateCalendarioCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Calendario.EmptyId)
            .WithMessage("Calendario id may not be empty");
    }
}