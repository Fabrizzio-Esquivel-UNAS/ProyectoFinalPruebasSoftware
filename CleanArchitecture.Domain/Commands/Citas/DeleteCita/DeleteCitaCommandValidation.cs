using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Citas.DeleteCita;

public sealed class DeleteCitaCommandValidation : AbstractValidator<DeleteCitaCommand>
{
    public DeleteCitaCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Cita.EmptyId)
            .WithMessage("Cita id may not be empty");
    }
}