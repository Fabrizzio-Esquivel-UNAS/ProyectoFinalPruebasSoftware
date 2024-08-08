using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.Contratos;
using CleanArchitecture.Shared.Contratos;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class ContratosContext : IContratosContext
{
    private readonly ContratosApi.ContratosApiClient _client;

    public ContratosContext(ContratosApi.ContratosApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<ContratoViewModel>> GetContratosByIds(IEnumerable<Guid> ids)
    {
        var request = new GetContratosByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Contratos.Select(contrato => new ContratoViewModel(
            Guid.Parse(contrato.Id),
            DateOnly.Parse(contrato.FechaInicio),
            DateOnly.Parse(contrato.FechaInicio)));
    }
}