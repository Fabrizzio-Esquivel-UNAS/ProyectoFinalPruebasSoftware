using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.GruposInvestigacion;
using CleanArchitecture.Shared.GrupoInvestigacion;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class GruposInvestigacionContext : IGruposInvestigacionContext
{
    private readonly GruposInvestigacionApi.GruposInvestigacionApiClient _client;

    public GruposInvestigacionContext(GruposInvestigacionApi.GruposInvestigacionApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<GrupoInvestigacionViewModel>> GetGruposInvestigacionByIds(IEnumerable<Guid> ids)
    {
        var request = new GetGruposInvestigacionByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.GruposInvestigacion.Select(grupoinvestigacion => new GrupoInvestigacionViewModel(
            Guid.Parse(grupoinvestigacion.Id),
            grupoinvestigacion.Nombre));
    }
}
