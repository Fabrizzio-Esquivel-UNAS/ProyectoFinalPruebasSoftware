using Newtonsoft.Json;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyPagination(
        [property: JsonProperty("count")] int Count,
        [property: JsonProperty("next_page")] string NextPage,
        [property: JsonProperty("previous_page")] string PreviousPage,
        [property: JsonProperty("next_page_token")] string NextPageToken,
        [property: JsonProperty("previous_page_token")] string PreviousPageToken
    );
}
