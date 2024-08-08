using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.Escuelas;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class EscuelasApiImplementation : EscuelasApi.EscuelasApiBase
{
    private readonly IEscuelaRepository _escuelaRepository;

    public EscuelasApiImplementation(IEscuelaRepository escuelaRepository)
    {
        _escuelaRepository = escuelaRepository;
    }

    public override async Task<GetEscuelasByIdsResult> GetByIds(
        GetEscuelasByIdsRequest request,
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

        var escuelas = await _escuelaRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(escuela => idsAsGuids.Contains(escuela.Id))
            .Select(escuela => new Escuela
            {
                Id = escuela.Id.ToString(),
                Nombre = escuela.Nombre,
                IsDeleted = escuela.Deleted
            })
            .ToListAsync();

        var result = new GetEscuelasByIdsResult();

        result.Escuelas.AddRange(escuelas);

        return result;
    }
}