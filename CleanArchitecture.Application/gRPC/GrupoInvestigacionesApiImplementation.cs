using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.GruposInvestigacion;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Application.gRPC;

public sealed class GruposInvestigacionApiImplementation : GruposInvestigacionApi.GruposInvestigacionApiBase
{
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;

    public GruposInvestigacionApiImplementation(IGrupoInvestigacionRepository grupoinvestigacionRepository)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
    }

    public override async Task<GetGruposInvestigacionByIdsResult> GetByIds(
        GetGruposInvestigacionByIdsRequest request,
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

        var gruposinvestigacion = await _grupoinvestigacionRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(grupoinvestigacion => idsAsGuids.Contains(grupoinvestigacion.Id))
            .Select(grupoinvestigacion => new GrupoInvestigacion
            {
                Id = grupoinvestigacion.Id.ToString(),
                Nombre = grupoinvestigacion.Nombre,
                IsDeleted = grupoinvestigacion.Deleted
            })
            .ToListAsync();

        var result = new GetGruposInvestigacionByIdsResult();

        result.GruposInvestigacion.AddRange(gruposinvestigacion);

        return result;
    }
}