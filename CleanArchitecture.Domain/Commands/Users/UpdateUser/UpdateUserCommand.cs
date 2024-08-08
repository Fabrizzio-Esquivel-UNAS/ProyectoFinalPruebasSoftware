using System;
using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Domain.Commands.Users.UpdateUser;

public sealed class UpdateUserCommand(
    Guid userId,
    Guid? escuelaId,
    string email,
    string firstName,
    string lastName,
    string telefono,
    byte notificaciones,
    UserRole role,
    AsesoriaEstado? estado = null) : CommandBase(userId)
{
    private static readonly UpdateUserCommandValidation s_validation = new();

    public Guid UserId { get; } = userId;
    public Guid? EscuelaId { get; } = escuelaId;
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Telefono { get; } = telefono;
    public UserRole Role { get; } = role;
    public byte Notificaciones { get; } = notificaciones;
    public AsesoriaEstado? AsesoriaEstado = estado;

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}