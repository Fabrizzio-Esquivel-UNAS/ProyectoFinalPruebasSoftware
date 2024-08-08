using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;

public class User(
    Guid id,
    Guid tenantId,
    Guid lineaInvestigacionId,
    Guid? escuelaId,
    string email,
    string firstName,
    string lastName,
    string password,
    string telefono,
    string codigo,
    UserRole role,
    byte notificaciones = 1,
    UserStatus status = UserStatus.Active,
    string preferencias = "",
    AsesoriaEstado asesoriaEstado = AsesoriaEstado.Normal) : Entity(id)
{
    public string Email { get; set; } = email;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Password { get; set; } = password;
    public string Telefono { get; set; } = telefono;
    public string Codigo { get; set; } = codigo;
    public byte Notificaciones { get; set; } = notificaciones;
    public string Preferencias { get; set; } = preferencias;
    public UserRole Role { get; set; } = role;
    public UserStatus Status { get; private set; } = status;
    public DateTimeOffset? LastLoggedinDate { get; set; }

    public string FullName => $"{FirstName}, {LastName}";

    public Guid TenantId { get; set; } = tenantId;
    public virtual Tenant Tenant { get; set; } = null!;
    public Guid? EscuelaId { get; set; } = escuelaId;
    public virtual Escuela Escuela { get; set; } = null!;

    public virtual Calendario? Calendario { get; set; } = null!;
    public AsesoriaEstado AsesoriaEstado { get; set; } = asesoriaEstado;
    [ForeignKey("LineaInvestigacion")]
    public Guid LineaInvestigacionId { get; set; } = lineaInvestigacionId;
    public virtual LineaInvestigacion? LineaInvestigacion { get; set; } = null!;

    public void SetInactive()
    {
        Status = UserStatus.Inactive;
    }

    public void SetActive()
    {
        Status = UserStatus.Active;
    }
}