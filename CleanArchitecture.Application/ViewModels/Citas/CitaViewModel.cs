using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.ViewModels.Citas;

public sealed class CitaViewModel
{
    public Guid Id { get; set; }
    public string EventoId { get; set; } = String.Empty;
    public Guid AsesorUserId { get; set; } = Guid.Empty;
    public Guid AsesoradoUserId { get; set; } = Guid.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin {  get; set; }
    public CitaEstado Estado { get; set; }
    public string DesarrolloAsesor { get; set; } = String.Empty;
    public string DesarrolloAsesorado { get; set; } = String.Empty;

    public static CitaViewModel FromCita(Cita cita)
    {
        return new CitaViewModel
        {
            Id = cita.Id,
            EventoId = cita.EventoId,
            AsesorUserId = cita.AsesorUserId,
            AsesoradoUserId = cita.AsesoradoUserId,
            FechaCreacion = cita.FechaCreacion,
            FechaInicio = cita.FechaInicio,
            FechaFin = cita.FechaFin,
            Estado = cita.Estado,
            DesarrolloAsesor = cita.DesarrolloAsesor,
            DesarrolloAsesorado = cita.DesarrolloAsesorado
        };
    }
}