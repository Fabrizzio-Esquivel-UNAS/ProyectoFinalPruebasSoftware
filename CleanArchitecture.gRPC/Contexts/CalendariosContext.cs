using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.Calendarios;
using CleanArchitecture.Shared.Calendarios;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class CalendariosContext : ICalendariosContext
{
    private readonly CalendariosApi.CalendariosApiClient _client;

    public CalendariosContext(CalendariosApi.CalendariosApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<CalendarioViewModel>> GetCalendariosByIds(IEnumerable<Guid> ids)
    {
        var request = new GetCalendariosByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Calendarios.Select(calendario => new CalendarioViewModel(
            Guid.Parse(calendario.Id),
            calendario.AccessToken,
            calendario.RefreshToken,
            calendario.UserUri,
            calendario.EventType));
    }
}