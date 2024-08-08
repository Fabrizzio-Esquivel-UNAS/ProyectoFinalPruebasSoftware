using System;

namespace CleanArchitecture.Domain.Commands.Calendarios.CreateCalendario;

public sealed class CreateCalendarioCommand : CommandBase
{
    private static readonly CreateCalendarioCommandValidation s_validation = new();

    public string AccessToken { get; }
    public string RefreshToken { get; }
    public DateTime? AccessTokenExpiration { get; }
    public DateTime? RefreshTokenExpiration { get; }

    public CreateCalendarioCommand(
        Guid calendarioId,
        string accessToken, 
        string refreshToken, 
        DateTime? accessTokenExpiration = null, 
        DateTime? refreshTokenExpiration = null) : base(calendarioId)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshTokenExpiration = refreshTokenExpiration;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}