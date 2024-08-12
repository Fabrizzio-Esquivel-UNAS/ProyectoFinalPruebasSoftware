using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Contratos.UpdateContrato;

public sealed class UpdateContratosCommandValidation : AbstractValidator<UpdateContratosCommand>
{
    public UpdateContratosCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.ContratoId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Contrato.EmptyId)
            .WithMessage("Contrato id may not be empty");
    }
}