using CleanArchitecture.Domain.Enums;
using System;

namespace CleanArchitecture.Application.ViewModels.Citas;

public sealed record UpdateCitaViewModel(
    Guid Id,
    CitaEstado Estado,
    string DesarrolloAsesor,
    string DesarrolloAsesorado);