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
            .WithMessage("HistorialCoordinador id may not be empty"); // E1
    }

    private void AddRuleForFechaFin()
    {
        RuleFor(cmd => cmd.FechaFin)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidFechaFin)
            .WithMessage("FechaFin is not a valid Fecha"); // E2
    }
}
