using System;

namespace CleanArchitecture.Shared.Contratos;

public sealed record ContratoViewModel(
    Guid Id,
    DateOnly FechaInicio,
    DateOnly? FechaFinal = null);