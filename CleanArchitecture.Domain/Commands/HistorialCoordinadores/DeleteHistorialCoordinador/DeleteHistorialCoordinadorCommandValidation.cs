using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.DeleteHistorialCoordinador;

public sealed class DeleteHistorialCoordinadorCommandValidation : AbstractValidator<DeleteHistorialCoordinadorCommand>
{
    public DeleteHistorialCoordinadorCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyId)
            .WithMessage("HistorialCoordinador id may not be empty"); // E1
    }
}