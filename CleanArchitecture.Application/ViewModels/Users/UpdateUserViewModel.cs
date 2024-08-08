using System;
using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.ViewModels.Users;

public sealed record UpdateUserViewModel(
    Guid Id,
    Guid LineaInvestigacionId,
    Guid EscuelaId,
    string Email,
    string FirstName,
    string LastName,
    string Telefono,
    byte Notificaciones,
    UserRole Role,
    AsesoriaEstado AsesoriaEstado);