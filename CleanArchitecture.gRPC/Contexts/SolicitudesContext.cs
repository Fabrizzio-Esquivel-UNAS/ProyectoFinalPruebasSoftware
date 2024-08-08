using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.Solicitudes;
using CleanArchitecture.Shared.Solicitud;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class SolicitudesContext : ISolicitudesContext
{
    private readonly SolicitudesApi.SolicitudesApiClient _client;

    public SolicitudesContext(SolicitudesApi.SolicitudesApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<SolicitudViewModel>> GetSolicitudesByIds(IEnumerable<Guid> ids)
    {
        var request = new GetSolicitudesByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Solicitudes.Select(solicitud => new SolicitudViewModel(
            Guid.Parse(solicitud.Id),
            solicitud.NumeroTesis,
            solicitud.IsDeleted));
    }
}