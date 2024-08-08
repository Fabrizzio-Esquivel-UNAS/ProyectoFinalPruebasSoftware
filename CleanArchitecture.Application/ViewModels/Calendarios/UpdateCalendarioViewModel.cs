using System;

namespace CleanArchitecture.Application.ViewModels.Calendarios;

public sealed record UpdateCalendarioViewModel(
    Guid Id, 
    string UserUri, 
    string AccessToken, 
    string RefreshToken, 
    DateTime? AccessTokenExpiration, 
    DateTime? RefreshTokenExpiration, 
    string? EventType, 
    string? EventsPageToken,
    string? SchedulingUrl);