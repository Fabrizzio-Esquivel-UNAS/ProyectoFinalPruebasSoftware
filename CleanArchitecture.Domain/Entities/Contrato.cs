using System;

namespace CleanArchitecture.Domain.Entities;

public class Contrato : Entity
{
    public Guid SolicitudId { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFinal { get; set; }
    public virtual Solicitud Solicitud { get; set; } = null!;

    public Contrato(
        Guid id,
        Guid solicitudId,
        DateOnly fechaInicio,
        DateOnly? fechaFinal = null) : base(id)
    {
        SolicitudId = solicitudId;
        FechaInicio = fechaInicio;
        FechaFinal = fechaFinal;
    }
}