using CleanArchitecture.Domain.Enums;
using System;

namespace CleanArchitecture.Application.ViewModels.Citas;

public sealed record CreateCitaViewModel(
    string EventoId, 
    Guid AsesorUserId, 
    string AsesoradoEmail,
    DateTime FechaCreacion,
    DateTime FechaInicio,
    DateTime FechaFin,
    CitaEstado Estado = CitaEstado.Programado);