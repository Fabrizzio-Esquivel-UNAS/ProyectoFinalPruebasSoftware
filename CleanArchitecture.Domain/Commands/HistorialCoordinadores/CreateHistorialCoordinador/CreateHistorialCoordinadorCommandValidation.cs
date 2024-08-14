using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;
using System;

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
            .WithMessage("HistorialCoordinador id may not be empty"); // E1
    }

    private void AddRuleForUserId()
    {
        RuleFor(cmd => cmd.UserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyUserId)
            .WithMessage("User id may not be empty"); // E2
    }

    private void AddRuleForGrupoInvestigacionId()
    {
        RuleFor(cmd => cmd.GrupoInvestigacionId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId)
            .WithMessage("GrupoInvestigacion id may not be empty"); // E3
    }

    private void AddRuleForFechaInicio()
    {
        RuleFor(cmd => cmd.FechaInicio)
            .Must(date => date != default(DateTime))
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio)
            .WithMessage("FechaInicio is not a valid Fecha"); // E4
    }
}