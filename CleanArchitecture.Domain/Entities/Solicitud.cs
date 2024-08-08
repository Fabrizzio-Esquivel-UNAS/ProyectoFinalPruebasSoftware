using System;
using System.Xml.Linq;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;

public class Solicitud : Entity
{
    public Guid AsesoradoUserId { get; set; }
    public virtual User AsesoradoUser { get; set; } = null!;
    public Guid AsesorUserId { get; set; }
    public virtual User AsesorUser { get; set; } = null!;
    public int NumeroTesis { get; set; }
    public string Mensaje { get; set; }
    public SolicitudStatus Estado {  get; private set; }
    public DateTimeOffset? FechaCreacion { get; private set; }
    public DateTimeOffset? FechaRespuesta { get; private set; }

    public Solicitud(Guid id,
        Guid asesoradoUserId,
        Guid asesorUserId,
        int numeroTesis,
        string mensaje,
        SolicitudStatus estado = SolicitudStatus.Pendiente,
        DateTimeOffset? fechaCreacion = null,
        DateTimeOffset? fechaRespuesta = null) : base(id)
    {
        AsesoradoUserId = asesoradoUserId;
        AsesorUserId = asesorUserId;
        NumeroTesis = numeroTesis;
        Mensaje = mensaje;
        Estado = estado;
        FechaCreacion = fechaCreacion ?? DateTimeOffset.Now;
        FechaRespuesta = fechaRespuesta;
    }

    public void SetAsesorado(Guid asesoradoId)
    {
        AsesoradoUserId = asesoradoId;
    }

    public void SetAsesor(Guid asesorId)
    {
        AsesorUserId = asesorId;
    }
    public void SetFechaCreacion(DateTimeOffset fechaCreacion)
    {
        FechaCreacion = fechaCreacion;
    }
    public void SetFechaRespuesta(DateTimeOffset fechaRespuesta)
    {
        FechaRespuesta = fechaRespuesta;
    }

    public void SetPendiente()
    {
        Estado = SolicitudStatus.Pendiente;
    }
    public void SetAceptado()
    {
        Estado = SolicitudStatus.Aceptado;
    }
    public void SetRechazado()
    {
        Estado = SolicitudStatus.Rechazado;
    }
    public void SetNumeroTesis(int numeroTesis)
    {
        NumeroTesis = numeroTesis;
    }
    public void SetEstado(SolicitudStatus estado)
    {
        Estado = estado;
    }
}