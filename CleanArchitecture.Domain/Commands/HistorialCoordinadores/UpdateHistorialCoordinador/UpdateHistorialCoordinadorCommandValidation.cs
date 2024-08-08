using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;

public sealed class UpdateHistorialCoordinadorCommandValidation : AbstractValidator<UpdateHistorialCoordinadorCommand>
{
    public UpdateHistorialCoordinadorCommandValidation()
    {
        AddRuleForId();
        AddRuleForFechaFin();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.HistorialCoordinadorId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyId)
            .WithMessage("HistorialCoordinador id may not be empty");
    }

    private void AddRuleForFechaFin()
    {
        RuleFor(cmd => cmd.FechaFin)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyFechaFin)
            .WithMessage("Fecha may not be empty")
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidFechaFin)
            .WithMessage("Fecha is not a valid Fecha");
    }
}

 