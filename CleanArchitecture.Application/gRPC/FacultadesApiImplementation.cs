using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.Facultades;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class FacultadesApiImplementation : FacultadesApi.FacultadesApiBase
{
    private readonly IFacultadRepository _facultadRepository;

    public FacultadesApiImplementation(IFacultadRepository facultadRepository)
    {
        _facultadRepository = facultadRepository;
    }

    public override async Task<GetFacultadesByIdsResult> GetByIds(
        GetFacultadesByIdsRequest request,
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

        var facultades = await _facultadRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(facultad => idsAsGuids.Contains(facultad.Id))
            .Select(facultad => new Facultad
            {
                Id = facultad.Id.ToString(),
                Nombre = facultad.Nombre,
                IsDeleted = facultad.Deleted
            })
            .ToListAsync();

        var result = new GetFacultadesByIdsResult();

        result.Facultades.AddRange(facultades);

        return result;
    }
}