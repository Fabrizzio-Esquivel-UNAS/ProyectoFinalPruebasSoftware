using System;

namespace CleanArchitecture.Application.ViewModels.LineasInvestigacion;

public sealed record UpdateLineaInvestigacionViewModel(
    Guid Id,
    string Nombre);