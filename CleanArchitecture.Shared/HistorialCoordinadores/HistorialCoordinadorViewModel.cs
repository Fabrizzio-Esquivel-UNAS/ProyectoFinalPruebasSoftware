using System;

namespace CleanArchitecture.Shared.HistorialCoordinador;

public sealed record HistorialCoordinadorViewModel(
    Guid Id,
    Guid UserId,
    Guid GrupoInvestigacionId,
    DateTime FechaInicio,
    DateTime FechaFin);
