using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class LineaInvestigacion : Entity
{
    public string Nombre { get; private set; }

    public Guid FacultadId { get; private set; }
    public virtual Facultad Facultad { get; set; } = null!;

    public Guid GrupoInvestigacionId { get; private set; }
    public virtual GrupoInvestigacion GrupoInvestigacion { get; set; } = null!;

    public LineaInvestigacion(
        Guid id,
        Guid facultadId,
        Guid grupoInvestigacionId,
        string nombre) : base(id)
    {
        FacultadId = facultadId;
        GrupoInvestigacionId = grupoInvestigacionId;
        Nombre = nombre;
    }

    public void SetNombre(string nombre)
    {
        Nombre = nombre;
    }

    public void SetFacultad(Guid facultadId)
    {
        FacultadId = facultadId;
    }

    public void SetGrupoInvestigacion(Guid grupoInvestigacionId)
    {
        GrupoInvestigacionId = grupoInvestigacionId;
    }
}