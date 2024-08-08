using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.ViewModels.Solicitudes;

public sealed class SolicitudViewModel
{
    public Guid Id { get; set; }
    public Guid AsesoradoUserId { get; set; } = Guid.Empty;

    public Guid AsesorUserId { get; set; } = Guid.Empty;
    public DateTimeOffset? FechaCreacion { get; set; }
    public DateTimeOffset? FechaRespuesta { get; set; }
    public int NumeroTesis { get; set; }
    public SolicitudStatus Estado { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static SolicitudViewModel FromSolicitud(Solicitud solicitud)
    {
        return new SolicitudViewModel
        {
            Id = solicitud.Id,
            AsesoradoUserId = solicitud.AsesoradoUserId,
            AsesorUserId = solicitud.AsesorUserId,
            NumeroTesis = solicitud.NumeroTesis,
            Estado = solicitud.Estado,
            FechaCreacion = solicitud.FechaCreacion,
            FechaRespuesta = solicitud.FechaRespuesta,
            Mensaje = solicitud.Mensaje,
            //Users = solicitud.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}