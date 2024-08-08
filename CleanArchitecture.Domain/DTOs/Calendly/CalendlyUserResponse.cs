using Newtonsoft.Json;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyUserResponse(
        [property: JsonProperty("resource")] CalendlyUser Resource
    );
}
