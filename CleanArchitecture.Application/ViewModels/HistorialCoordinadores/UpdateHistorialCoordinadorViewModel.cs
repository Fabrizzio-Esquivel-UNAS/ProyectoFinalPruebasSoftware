using System;

namespace CleanArchitecture.Application.ViewModels.HistorialCoordinadores;

public sealed record UpdateHistorialCoordinadorViewModel(
    Guid Id,
    Guid UserId,
    Guid GrupoInvestigacionId,
    DateTime FechaInicio,
    DateTime FechaFin);