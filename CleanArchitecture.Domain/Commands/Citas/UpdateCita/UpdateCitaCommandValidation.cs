using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Citas.UpdateCita;

public sealed class UpdateCitaCommandValidation : AbstractValidator<UpdateCitaCommand>
{
    public UpdateCitaCommandValidation()
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