using Newtonsoft.Json;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyToken(
        [property: JsonProperty("access_token")] string AccessToken,
        [property: JsonProperty("refresh_token")] string RefreshToken,
        [property: JsonProperty("expires_in")] int ExpiresIn
    );
}
