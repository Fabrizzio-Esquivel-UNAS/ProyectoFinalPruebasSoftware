using System;

namespace CleanArchitecture.Application.ViewModels.HistorialCoordinadores;

public sealed record CreateHistorialCoordinadorViewModel(
    Guid Id,
    Guid UserId,
    Guid GrupoInvestigacionId,
    DateTime FechaInicio,
    DateTime FechaFin);