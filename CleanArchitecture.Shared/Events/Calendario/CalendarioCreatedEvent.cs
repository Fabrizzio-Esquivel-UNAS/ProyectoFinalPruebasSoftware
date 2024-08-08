using System;

namespace CleanArchitecture.Shared.Events.Calendario;

public sealed class CalendarioCreatedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string AccessToken { get; }
    public string RefreshToken { get; }
    public string UserUri { get; }

    public CalendarioCreatedEvent(Guid calendarioId, Guid userId, string accessToken, string refreshToken, string userUri) : base(calendarioId)
    {
        UserId = userId;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UserUri = userUri;
    }
}