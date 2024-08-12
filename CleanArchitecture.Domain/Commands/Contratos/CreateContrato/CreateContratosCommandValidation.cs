using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Contratos.CreateContrato;

public sealed class CreateContratosCommandValidation : AbstractValidator<CreateContratosCommand>
{
    public CreateContratosCommandValidation()
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