using System;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.ViewModels.Solicitudes;
public sealed record CreateSolicitudViewModel(
    Guid AsesoradoUserId, 
    Guid AsesorUserId,
    int NumeroTesis, 
    string Mensaje);