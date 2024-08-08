using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace CleanArchitecture.Application.ViewModels.Users;

public sealed record CreateUserViewModel(
    Guid LineaInvestigacionId,
    Guid EscuelaId,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string Telefono,
    string Codigo,
    UserRole Role);