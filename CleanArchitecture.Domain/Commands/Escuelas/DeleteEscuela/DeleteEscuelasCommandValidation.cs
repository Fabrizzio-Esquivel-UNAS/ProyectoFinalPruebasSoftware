using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Escuelas.DeleteEscuela;

public sealed class DeleteEscuelasCommandValidation : AbstractValidator<DeleteEscuelasCommand>
{
    public DeleteEscuelasCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Escuela.EmptyId)
            .WithMessage("Escuela id may not be empty");
    }
}