using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.Citas;
using CleanArchitecture.Shared.Citas;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class CitasContext : ICitasContext
{
    private readonly CitasApi.CitasApiClient _client;

    public CitasContext(CitasApi.CitasApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<CitaViewModel>> GetCitasByIds(IEnumerable<Guid> ids)
    {
        var request = new GetCitasByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Citas.Select(cita => new CitaViewModel(
            Guid.Parse(cita.Id),
            cita.EventoId));
    }
}