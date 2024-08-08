using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Contratos.DeleteContrato;

public sealed class DeleteContratoCommandValidation : AbstractValidator<DeleteContratoCommand>
{
    public DeleteContratoCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Contrato.EmptyId)
            .WithMessage("Contrato id may not be empty");
    }
}