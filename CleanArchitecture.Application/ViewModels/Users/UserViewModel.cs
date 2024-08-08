using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.ViewModels.Users;

public sealed class UserViewModel
{
    public Guid Id { get; set; }
    public Guid LineaInvestigacionId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string Preferencias {  get; set; } = string.Empty;
    public byte Notificaciones { get; set; }
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public AsesoriaEstado AsesoriaEstado { get; set; }
    public Guid? EscuelaId { get; set; }
    public Guid? AsesorId { get; set; } = null;
    public Guid? GrupoInvestigacionId { get; set; } = null;

    public static UserViewModel FromUser(User user, Guid? asesorId=null)
    {
        return new UserViewModel
        {
            Id = user.Id,
            LineaInvestigacionId = user.LineaInvestigacionId,
            AsesoriaEstado = user.AsesoriaEstado,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Telefono = user.Telefono,
            Codigo = user.Codigo,
            Notificaciones = user.Notificaciones,
            Preferencias = user.Preferencias,
            Role = user.Role,
            Status = user.Status,
            EscuelaId = user.EscuelaId,
            AsesorId = asesorId
        };
    }
}