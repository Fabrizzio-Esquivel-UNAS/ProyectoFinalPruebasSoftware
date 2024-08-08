using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Facultades.DeleteFacultad;

public sealed class DeleteFacultadCommandValidation : AbstractValidator<DeleteFacultadCommand>
{
    public DeleteFacultadCommandValidation()
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