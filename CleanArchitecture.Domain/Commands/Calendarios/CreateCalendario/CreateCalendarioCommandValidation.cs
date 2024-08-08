using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Calendarios.CreateCalendario;

public sealed class CreateCalendarioCommandValidation : AbstractValidator<CreateCalendarioCommand>
{
    public CreateCalendarioCommandValidation()
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