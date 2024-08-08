using System;

namespace CleanArchitecture.Shared.Solicitud;

public sealed record SolicitudViewModel(
    Guid Id,
    int NumeroTesis,
    bool IsDeleted);