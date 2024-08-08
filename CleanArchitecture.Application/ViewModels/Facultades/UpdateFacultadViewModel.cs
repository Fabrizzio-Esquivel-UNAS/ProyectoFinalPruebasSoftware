using System;

namespace CleanArchitecture.Application.ViewModels.Facultades;

public sealed record UpdateFacultadViewModel(
    Guid Id,
    string Nombre);