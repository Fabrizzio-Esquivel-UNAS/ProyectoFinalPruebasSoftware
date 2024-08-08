using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities;

public class HistorialCoordinador : Entity
{

    public Guid UserId { get; private set; }
    public virtual User User { get; private set; } = null!;

    public Guid GrupoInvestigacionId { get; private set; }
    public virtual GrupoInvestigacion GrupoInvestigacion { get; private set; } = null!;

    public DateTime FechaInicio { get; private set; }
    public DateTime? FechaFin { get; private set; }

    public HistorialCoordinador(
        Guid id,
        Guid userId,
        Guid grupoInvestigacionId,
        DateTime fechaInicio,
        DateTime? fechaFin) : base(id)
    {
        UserId = userId;
        GrupoInvestigacionId = grupoInvestigacionId;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;

    }

    public void SetUser(Guid userId)
    {
        userId = UserId;
    }
    public void SetGrupoInvestigacion(Guid grupoInvestigacionId)
    {
        GrupoInvestigacionId = grupoInvestigacionId;
    }

    public void SetFechaInicio(DateTime fechaInicio)
    {
        FechaInicio = fechaInicio;
    }

    public void SetFechaFin(DateTime fechaFin)
    {
        FechaFin = fechaFin;
    }

}