using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.HistorialCoordinadores;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class HistorialCoordinadoresApiImplementation : HistorialCoordinadoresApi.HistorialCoordinadoresApiBase
{
    private readonly IHistorialCoordinadorRepository _historialcoordinadorRepository;

    public HistorialCoordinadoresApiImplementation(IHistorialCoordinadorRepository historialcoordinadorRepository)
    {
        _historialcoordinadorRepository = historialcoordinadorRepository;
    }

    public override async Task<GetHistorialCoordinadoresByIdsResult> GetByIds(
        GetHistorialCoordinadoresByIdsRequest request,
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

        var historialcoordinadores = await _historialcoordinadorRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(historialcoordinador => idsAsGuids.Contains(historialcoordinador.Id))
            .Select(historialcoordinador => new HistorialCoordinador
            {
                Id = historialcoordinador.Id.ToString(),
                UserId = historialcoordinador.UserId.ToString(),
                GrupoinvestigacionId = historialcoordinador.GrupoInvestigacionId.ToString(),
                Fechainicio = historialcoordinador.FechaInicio.ToString(),
                Fechafin = historialcoordinador.FechaFin.ToString()

         

            })
            .ToListAsync();

        var result = new GetHistorialCoordinadoresByIdsResult();

        result.Historialcoordinadores.AddRange(historialcoordinadores);

        return result;
    }
}