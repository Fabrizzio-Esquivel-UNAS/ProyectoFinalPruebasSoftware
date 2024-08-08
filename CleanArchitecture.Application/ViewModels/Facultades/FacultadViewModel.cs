using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.Facultades;

public sealed class FacultadViewModel
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static FacultadViewModel FromFacultades(Facultad facultad)
    {
        return new FacultadViewModel
        {
            Id = facultad.Id,
            Nombre = facultad.Nombre
        };
    }
}