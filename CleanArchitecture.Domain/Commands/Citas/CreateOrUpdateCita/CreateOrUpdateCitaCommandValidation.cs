using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Citas.CreateCita;

public sealed class CreateOrUpdateCitaCommandValidation : AbstractValidator<CreateOrUpdateCitaCommand>
{
    public CreateOrUpdateCitaCommandValidation()
    {
        AddRuleForId();
        AddRuleForAsesorUserId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Cita.EmptyId)
            .WithMessage("Cita id may not be empty");
    }

    private void AddRuleForAsesorUserId()
    {
        RuleFor(cmd => cmd.AsesorUserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Tenant.EmptyId)
            .WithMessage("Asesorado id may not be empty");
    }
}