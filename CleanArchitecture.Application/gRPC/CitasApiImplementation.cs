using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.Citas;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class CitasApiImplementation : CitasApi.CitasApiBase
{
    private readonly ICitaRepository _citaRepository;

    public CitasApiImplementation(ICitaRepository citaRepository)
    {
        _citaRepository = citaRepository;
    }

    public override async Task<GetCitasByIdsResult> GetByIds(
        GetCitasByIdsRequest request,
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

        var citas = await _citaRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(cita => idsAsGuids.Contains(cita.Id))
            .Select(cita => new Cita
            {
                Id = cita.Id.ToString(),
                EventoId = cita.EventoId,
                IsDeleted = cita.Deleted
            })
            .ToListAsync();

        var result = new GetCitasByIdsResult();

        result.Citas.AddRange(citas);

        return result;
    }
}