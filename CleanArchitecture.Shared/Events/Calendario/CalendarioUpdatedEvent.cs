using System;

namespace CleanArchitecture.Shared.Events.Calendario;

public sealed class CalendarioUpdatedEvent : DomainEvent
{
    public string AccessToken { get; }
    public DateTime? AccessTokenExpiration { get; }
    public string RefreshToken { get; }
    public DateTime? RefreshTokenExpiration { get; }
    public string UserUri { get; }
    public string? EventType { get; }
    public string? EventsPageToken { get; }

    public CalendarioUpdatedEvent(
        Guid calendarioId, 
        string userUri, 
        string accessToken, 
        string refreshToken, 
        DateTime? accessTokenExpiration, 
        DateTime? refreshTokenExpiration, 
        string? eventType, 
        string? eventsPageToken
        ) : base(calendarioId)
    {
        UserUri = userUri;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshTokenExpiration = refreshTokenExpiration;
        EventType = eventType;
        EventsPageToken = eventsPageToken;
    }
}