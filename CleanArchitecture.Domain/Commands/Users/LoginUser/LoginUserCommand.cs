using System;
using CleanArchitecture.Domain.DTOs;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Users.LoginUser;

public sealed class LoginUserCommand : CommandBase,
    IRequest<TokenResponse>
{
    private static readonly LoginUserCommandValidation s_validation = new();

    public string Email { get; set; }
    public string Password { get; set; }


    public LoginUserCommand(
        string email,
        string password) : base(Guid.NewGuid())
    {
        Email = email;
        Password = password;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}