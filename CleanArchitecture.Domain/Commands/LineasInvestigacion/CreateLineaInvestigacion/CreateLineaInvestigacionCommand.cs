using System;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.CreateLineaInvestigacion;

public sealed class CreateLineaInvestigacionCommand : CommandBase
{
    private static readonly CreateLineaInvestigacionCommandValidation s_validation = new();

    public Guid FacultadId { get; }
    public Guid GrupoInvestigacionId { get; }
    public string Nombre { get; }

    public CreateLineaInvestigacionCommand(
        Guid lineainvestigacionId,
        Guid facultadId,
        Guid grupoinvestigacionId,
        string nombre) : base(lineainvestigacionId)
    {
        FacultadId = facultadId;
        GrupoInvestigacionId = grupoinvestigacionId;
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}