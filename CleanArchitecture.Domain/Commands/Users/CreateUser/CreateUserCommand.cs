using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace CleanArchitecture.Domain.Commands.Users.CreateUser;

public sealed class CreateUserCommand(
    Guid userId,
    Guid lineaInvestigacionId,
    Guid? escuelaId,
    string email,
    string firstName,
    string lastName,
    string password,
    string telefono,
    string codigo,
    UserRole role) : CommandBase(userId)
{
    private static readonly CreateUserCommandValidation s_validation = new();

    public Guid UserId { get; } = userId;
    public Guid LineaInvestigacionId { get; } = lineaInvestigacionId;
    public Guid? EscuelaId { get; } = escuelaId;
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Password { get; } = password;
    public string Telefono { get; } = telefono;
    public string Codigo { get; } = codigo;
    public UserRole Role { get; } = role;

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}