using Newtonsoft.Json;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyEventTypeResponse(
        [property: JsonProperty("collection")] List<CalendlyEventType> Collection,
        [property: JsonProperty("pagination")] CalendlyPagination Pagination
    );
}
