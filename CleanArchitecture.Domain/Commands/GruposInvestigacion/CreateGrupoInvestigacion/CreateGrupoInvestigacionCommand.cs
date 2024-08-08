using CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;
using CleanArchitecture.Domain.Entities;
using System;

namespace CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;
public sealed class CreateGrupoInvestigacionCommand : CommandBase
{
    private static readonly CreateGrupoInvestigacionCommandValidation s_validation = new();

    public string Nombre { get; }
    public Guid? CoordinadorUserId { get; }

    public CreateGrupoInvestigacionCommand(
        Guid grupoInvestigacionId,
        string nombre,
        Guid? coordinadorUserId) : base(grupoInvestigacionId)
    {
        Nombre = nombre;
        CoordinadorUserId = coordinadorUserId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}

