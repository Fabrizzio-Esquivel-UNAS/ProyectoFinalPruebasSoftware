using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.Escuelas;

public sealed class EscuelaViewModel
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public Guid FacultadId { get; set; } = Guid.Empty;

    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static EscuelaViewModel FromEscuelas(Escuela escuela)
    {
        return new EscuelaViewModel
        {
            Id = escuela.Id,
            Nombre = escuela.Nombre,
            FacultadId = escuela.FacultadId,
        };
    }
}