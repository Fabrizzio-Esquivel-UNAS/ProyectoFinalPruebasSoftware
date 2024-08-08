using System;

namespace CleanArchitecture.Application.ViewModels.Calendarios;

public sealed record CreateCalendarioViewModel(
    string AccessToken, 
    string RefreshToken,
    int? AccessTokenExpiration = null,
    int? RefreshTokenExpiration = null);