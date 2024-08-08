using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Calendarios.DeleteCalendario;

public sealed class DeleteCalendarioCommandValidation : AbstractValidator<DeleteCalendarioCommand>
{
    public DeleteCalendarioCommandValidation()
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