using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.Calendarios;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class CalendariosApiImplementation : CalendariosApi.CalendariosApiBase
{
    private readonly ICalendarioRepository _calendarioRepository;

    public CalendariosApiImplementation(ICalendarioRepository calendarioRepository)
    {
        _calendarioRepository = calendarioRepository;
    }

    public override async Task<GetCalendariosByIdsResult> GetByIds(
        GetCalendariosByIdsRequest request,
        ServerCallContext context)
    {
        var idsAsGuids = new List<Guid>(request.Ids.Count);

        foreach (var id in request.Ids)
        {
            if (Guid.TryParse(id, out var parsed))
            {
                idsAsGuids.Add(parsed);
            }
        }

        var calendarios = await _calendarioRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(calendario => idsAsGuids.Contains(calendario.Id))
            .Select(calendario => new Calendario
            {
                Id = calendario.Id.ToString(),
                AccessToken = calendario.AccessToken,
                RefreshToken = calendario.RefreshToken,
                UserUri = calendario.UserUri,
                EventType = calendario.EventType,
                IsDeleted = calendario.Deleted
            })
            .ToListAsync();

        var result = new GetCalendariosByIdsResult();

        result.Calendarios.AddRange(calendarios);

        return result;
    }
}