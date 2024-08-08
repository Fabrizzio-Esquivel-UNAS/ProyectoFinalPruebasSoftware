using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.Escuelas;
using CleanArchitecture.Shared.Escuela;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class EscuelasContext : IEscuelasContext
{
    private readonly EscuelasApi.EscuelasApiClient _client;

    public EscuelasContext(EscuelasApi.EscuelasApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<EscuelaViewModel>> GetEscuelasByIds(IEnumerable<Guid> ids)
    {
        var request = new GetEscuelasByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Escuelas.Select(escuela => new EscuelaViewModel(
            Guid.Parse(escuela.Id),
            escuela.Nombre));
    }
}