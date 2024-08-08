using Newtonsoft.Json;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyUser(
        [property: JsonProperty("uri")] string Uri,
        [property: JsonProperty("email")] string Email,
        [property: JsonProperty("scheduling_url")] string Scheduling_url
    );
}
