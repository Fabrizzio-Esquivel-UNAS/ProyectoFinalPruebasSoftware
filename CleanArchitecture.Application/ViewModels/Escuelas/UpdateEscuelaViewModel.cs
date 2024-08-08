using System;

namespace CleanArchitecture.Application.ViewModels.Escuelas;

public sealed record UpdateEscuelaViewModel(
    Guid Id,
    string Nombre);