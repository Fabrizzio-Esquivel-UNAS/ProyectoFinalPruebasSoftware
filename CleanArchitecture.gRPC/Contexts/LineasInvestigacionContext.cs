using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.LineasInvestigacion;
using CleanArchitecture.Shared.LineaInvestigacion;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class LineasInvestigacionContext : ILineasInvestigacionContext
{
    private readonly LineasInvestigacionApi.LineasInvestigacionApiClient _client;

    public LineasInvestigacionContext(LineasInvestigacionApi.LineasInvestigacionApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<LineaInvestigacionViewModel>> GetLineasInvestigacionByIds(IEnumerable<Guid> ids)
    {
        var request = new GetLineasInvestigacionByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.LineasInvestigacion.Select(lineainvestigacion => new LineaInvestigacionViewModel(
            Guid.Parse(lineainvestigacion.Id),
            lineainvestigacion.Nombre));
    }
}