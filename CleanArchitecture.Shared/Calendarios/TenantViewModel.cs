using System;

namespace CleanArchitecture.Shared.Calendarios;

public sealed record CalendarioViewModel(
    Guid Id,
    string AccessToken,
    string RefreshToken,
    string UserUri,
    string EventType);