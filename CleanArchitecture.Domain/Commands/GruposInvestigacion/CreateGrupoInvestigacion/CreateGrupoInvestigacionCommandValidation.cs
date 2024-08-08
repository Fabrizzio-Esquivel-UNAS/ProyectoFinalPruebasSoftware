using CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;

public sealed class CreateGrupoInvestigacionCommandValidation : AbstractValidator<CreateGrupoInvestigacionCommand>
{
    public CreateGrupoInvestigacionCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.GrupoInvestigacion.EmptyId)
            .WithMessage("GrupoInvestigacion id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Nombre)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.GrupoInvestigacion.EmptyNombre)
            .WithMessage("Nombre may not be empty")
            .MaximumLength(MaxLengths.GrupoInvestigacion.Nombre)
            .WithErrorCode(DomainErrorCodes.GrupoInvestigacion.NombreExceedsMaxLength)
            .WithMessage($"Nombre may not be longer than {MaxLengths.GrupoInvestigacion.Nombre} characters");
    }
}
