using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.Calendarios;

public sealed class CalendarioViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public DateTime? AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiration { get; set; }
    public string UserUri { get; set; } = string.Empty;
    public string? EventType { get; set; } = string.Empty;
    public string? EventsPageToken { get; set; } = string.Empty;
    public string? SchedulingUrl { get; set; } = string.Empty;

    public static CalendarioViewModel FromCalendario(Calendario calendario)
    {
        return new CalendarioViewModel
        {
            Id = calendario.Id,
            UserId = calendario.UserId,
            AccessToken = calendario.AccessToken,
            AccessTokenExpiration = calendario.AccessTokenExpiration,
            RefreshToken = calendario.RefreshToken,
            RefreshTokenExpiration = calendario.RefreshTokenExpiration,
            UserUri = calendario.UserUri,
            EventType = calendario.EventType,
            EventsPageToken = calendario.EventsPageToken,
            SchedulingUrl = calendario.SchedulingUrl,
        };
    }
}