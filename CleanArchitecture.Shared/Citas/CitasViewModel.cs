using System;

namespace CleanArchitecture.Shared.Citas;

public sealed record CitaViewModel(
    Guid Id,
    string EventoId);