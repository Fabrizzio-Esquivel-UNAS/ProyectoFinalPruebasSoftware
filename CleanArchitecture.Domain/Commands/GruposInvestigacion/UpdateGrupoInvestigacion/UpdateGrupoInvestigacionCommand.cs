using CleanArchitecture.Domain.Commands.GruposInvestigacion.UpdateGrupoInvestigacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Commands.GruposInvestigacion.UpdateGrupoInvestigacion;
public sealed class UpdateGrupoInvestigacionCommand : CommandBase
{
    private static readonly UpdateGrupoInvestigacionCommandValidation s_validation = new();

    public string Nombre { get; }
    public Guid? CoordinadorUserId {  get; }
    public UpdateGrupoInvestigacionCommand(Guid grupoinvestigacionId, string nombre, Guid? coordinadorUserId) : base(grupoinvestigacionId)
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
