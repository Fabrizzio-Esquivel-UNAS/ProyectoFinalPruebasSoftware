using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.LineasInvestigacion;

public sealed class LineaInvestigacionViewModel
{
    public Guid Id { get; set; }
    public Guid FacultadId { get; set; }
    public Guid GrupoInvestigacionId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static LineaInvestigacionViewModel FromLineaInvestigacion(LineaInvestigacion lineainvestigacion)
    {
        return new LineaInvestigacionViewModel
        {
            Id = lineainvestigacion.Id,
            FacultadId = lineainvestigacion.FacultadId,
            GrupoInvestigacionId = lineainvestigacion.GrupoInvestigacionId,
            Nombre = lineainvestigacion.Nombre,
            //Users = lineainvestigacion.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}