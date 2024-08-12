using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Facultades.DeleteFacultad;

public sealed class DeleteFacultadesCommandValidation : AbstractValidator<DeleteFacultadesCommand>
{
    public DeleteFacultadesCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Facultad.EmptyId)
            .WithMessage("Facultad id may not be empty");
    }
}