using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class Calendario : Entity
{
    public string UserUri { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? AccessTokenExpiration { get; set; }
    public DateTime? RefreshTokenExpiration { get; set;}
    public string? EventType {  get; set; }
    public string? EventsPageToken { get; set; }
    public string? SchedulingUrl { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
        public Calendario(
        Guid id,
        Guid userId,
        string userUri,
        string accessToken,
        string refreshToken,
        DateTime? accessTokenExpiration = null,
        DateTime? refreshTokenExpiration = null,
        string? eventType = null,
        string? eventsPageToken = null,
        string? schedulingUrl = null) : base(id)
    {
        UserId = userId;
        AccessToken = accessToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
        UserUri = userUri;
        EventType = eventType;
        EventsPageToken = eventsPageToken;
        SchedulingUrl = schedulingUrl;
    }
}