using System;

namespace CleanArchitecture.Shared.LineaInvestigacion;

public sealed record LineaInvestigacionViewModel(
    Guid Id,
    string Nombre);