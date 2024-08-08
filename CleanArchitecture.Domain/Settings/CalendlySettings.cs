namespace CleanArchitecture.Domain.Settings;

public sealed class CalendlySettings
{
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string WebhookSigningKey { get; set; } = null!;
    public string RedirectUri { get; set; } = null!;
    public string UserEndpoint { get; set; } = null!;
    public string AuthorizationEndpoint { get; set; } = null!;
    public string TokenEndpoint { get; set; } = null!;
    public string EventTypesEndpoint { get; set; } = null!;
    public string ScheduledEventsEndpoint { get; set; } = null!;
    public string InviteesEndpoint { get; set; } = null!;}
