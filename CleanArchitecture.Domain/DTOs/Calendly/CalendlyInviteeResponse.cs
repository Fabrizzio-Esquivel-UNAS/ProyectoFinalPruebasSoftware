using Newtonsoft.Json;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyInviteeResponse(
        [property: JsonProperty("collection")] List<CalendlyInvitee> Collection
    );
}
