using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;

public sealed class UpdateSolicitudCommandValidation : AbstractValidator<UpdateSolicitudCommand>
{
    public UpdateSolicitudCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Solicitud.EmptyId)
            .WithMessage("Solicitud id may not be empty");
    }
}