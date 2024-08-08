using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.ViewModels.HistorialCoordinadores;

public sealed class HistorialCoordinadorViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GrupoInvestigacionId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
   

    public static HistorialCoordinadorViewModel FromHistorialCoordinador(HistorialCoordinador historialcoordinador)
    {
        return new HistorialCoordinadorViewModel
        {
            Id = historialcoordinador.Id,
            UserId = historialcoordinador.UserId,
            GrupoInvestigacionId = historialcoordinador.GrupoInvestigacionId,
            FechaInicio = historialcoordinador.FechaInicio,
            FechaFin = historialcoordinador.FechaFin
        };
    }
}