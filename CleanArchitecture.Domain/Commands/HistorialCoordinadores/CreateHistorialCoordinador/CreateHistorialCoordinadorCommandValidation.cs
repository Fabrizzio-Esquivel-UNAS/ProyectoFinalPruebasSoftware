using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;

public sealed class CreateHistorialCoordinadorCommandValidation : AbstractValidator<CreateHistorialCoordinadorCommand>
{
    public CreateHistorialCoordinadorCommandValidation()
    {
        AddRuleForId();
        AddRuleForGrupoInvestigacionId();
        AddRuleForUserId();
        AddRuleForFechaInicio();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.HistorialCoordinadorId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyId)
            .WithMessage("HistorialCoordinador id may not be empty");
    }

    private void AddRuleForUserId()
    {
        RuleFor(cmd => cmd.UserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyUserId)
            .WithMessage("User id may not be empty")
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidUserId)
            .WithMessage("User Id is not a valid User Id");
    }

    private void AddRuleForGrupoInvestigacionId()
    {
        RuleFor(cmd => cmd.GrupoInvestigacionId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId)
            .WithMessage("Grupo Investigacion id may not be empty")
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidGrupoInvestigacionId)
            .WithMessage("Grupo Investigacion Id is not a valid User Id");
    }

    private void AddRuleForFechaInicio()
    {
        RuleFor(cmd => cmd.FechaInicio)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyFechaInicio)
            .WithMessage("FechaInicio may not be empty")
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio)
            .WithMessage("FechaInicio is not a valid Fecha");
    }
}

  