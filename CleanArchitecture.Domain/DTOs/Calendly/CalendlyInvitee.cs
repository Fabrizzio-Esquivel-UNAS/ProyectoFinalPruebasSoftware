using Newtonsoft.Json;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyInvitee(
        [property: JsonProperty("email")] string Email
    );
}
