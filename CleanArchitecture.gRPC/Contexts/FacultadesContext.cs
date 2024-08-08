using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.Facultades;
using CleanArchitecture.Shared.Facultad;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class FacultadesContext : IFacultadesContext
{
    private readonly FacultadesApi.FacultadesApiClient _client;

    public FacultadesContext(FacultadesApi.FacultadesApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<FacultadViewModel>> GetFacultadesByIds(IEnumerable<Guid> ids)
    {
        var request = new GetFacultadesByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Facultades.Select(facultad => new FacultadViewModel(
            Guid.Parse(facultad.Id),
            facultad.Nombre));
    }
}