using Newtonsoft.Json;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyScheduledEventResponse(
        [property: JsonProperty("collection")] List<CalendlyScheduledEvent> Collection,
        [property: JsonProperty("pagination")] CalendlyPagination Pagination
    );
}
