using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.Contratos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class ContratosApiImplementation : ContratosApi.ContratosApiBase
{
    private readonly IContratoRepository _contratoRepository;

    public ContratosApiImplementation(IContratoRepository contratoRepository)
    {
        _contratoRepository = contratoRepository;
    }

    public override async Task<GetContratosByIdsResult> GetByIds(
        GetContratosByIdsRequest request,
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

        var contratos = await _contratoRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(contrato => idsAsGuids.Contains(contrato.Id))
            .Select(contrato => new Contrato
            {
                Id = contrato.Id.ToString(),
                FechaInicio = contrato.FechaInicio.ToString(),
                FechaFinal = contrato.FechaFinal.ToString(),
            })
            .ToListAsync();

        var result = new GetContratosByIdsResult();

        result.Contratos.AddRange(contratos);

        return result;
    }
}