using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.LineasInvestigacion;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class LineasInvestigacionApiImplementation : LineasInvestigacionApi.LineasInvestigacionApiBase
{
    private readonly ILineaInvestigacionRepository _lineainvestigacionRepository;

    public LineasInvestigacionApiImplementation(ILineaInvestigacionRepository lineainvestigacionRepository)
    {
        _lineainvestigacionRepository = lineainvestigacionRepository;
    }

    public override async Task<GetLineasInvestigacionByIdsResult> GetByIds(
        GetLineasInvestigacionByIdsRequest request,
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

        var lineasinvestigacion = await _lineainvestigacionRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(lineainvestigacion => idsAsGuids.Contains(lineainvestigacion.Id))
            .Select(lineainvestigacion => new LineaInvestigacion
            {
                Id = lineainvestigacion.Id.ToString(),
                Nombre = lineainvestigacion.Nombre,
                IsDeleted = lineainvestigacion.Deleted
            })
            .ToListAsync();

        var result = new GetLineasInvestigacionByIdsResult();

        result.LineasInvestigacion.AddRange(lineasinvestigacion);

        return result;
    }
}