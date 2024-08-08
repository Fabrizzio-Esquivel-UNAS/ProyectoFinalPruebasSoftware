using System;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.ViewModels.Solicitudes;

public sealed record UpdateSolicitudViewModel(
    Guid Id,
    SolicitudStatus Estado);