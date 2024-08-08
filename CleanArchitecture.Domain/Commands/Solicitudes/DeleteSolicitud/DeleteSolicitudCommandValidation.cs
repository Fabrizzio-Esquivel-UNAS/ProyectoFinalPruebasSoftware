using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Solicitudes.DeleteSolicitud;

public sealed class DeleteSolicitudCommandValidation : AbstractValidator<DeleteSolicitudCommand>
{
    public DeleteSolicitudCommandValidation()
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