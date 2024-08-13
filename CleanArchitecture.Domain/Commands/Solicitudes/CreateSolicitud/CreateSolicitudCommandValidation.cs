using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;

public sealed class CreateSolicitudCommandValidation : AbstractValidator<CreateSolicitudCommand>
{
    public CreateSolicitudCommandValidation()
    {
        AddRuleForId();
        AddRuleForAsesoradoUserId();
        AddRuleForAsesorUserId();
        AddRuleForNumeroTesis();
        AddRuleForMensaje();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Solicitud.EmptyId)
            .WithMessage("Solicitud id may not be empty");
    }

    private void AddRuleForAsesoradoUserId()
    {
        RuleFor(cmd => cmd.AsesoradoUserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.User.EmptyId)
            .WithMessage("Asesorado user id may not be empty");
    }

    private void AddRuleForAsesorUserId()
    {
        RuleFor(cmd => cmd.AsesorUserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.User.EmptyId)
            .WithMessage("Asesor user id may not be empty");
    }

    private void AddRuleForNumeroTesis()
    {
        RuleFor(cmd => cmd.NumeroTesis)
            .GreaterThan(0)
            .WithErrorCode(DomainErrorCodes.Solicitud.InvalidNumeroTesis)
            .WithMessage("Numero tesis must be greater than zero");
    }

    private void AddRuleForMensaje()
    {
        RuleFor(cmd => cmd.Mensaje)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Solicitud.EmptyMensaje)
            .WithMessage("Mensaje may not be empty");
    }
}
