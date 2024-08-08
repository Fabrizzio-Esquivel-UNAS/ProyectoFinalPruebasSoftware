using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.HistorialCoordinadores;
using CleanArchitecture.Shared.HistorialCoordinador;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class HistorialCoordinadoresContext : IHistorialCoordinadoresContext
{
    private readonly HistorialCoordinadoresApi.HistorialCoordinadoresApiClient _client;

    public HistorialCoordinadoresContext(HistorialCoordinadoresApi.HistorialCoordinadoresApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<HistorialCoordinadorViewModel>> GetHistorialCoordinadoresByIds(IEnumerable<Guid> ids)
    {
        var request = new GetHistorialCoordinadoresByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Historialcoordinadores.Select(historialcoordinador => new HistorialCoordinadorViewModel(
            Guid.Parse(historialcoordinador.Id),
            Guid.Parse(historialcoordinador.UserId),
            Guid.Parse(historialcoordinador.GrupoinvestigacionId),
            DateTime.Parse(historialcoordinador.Fechainicio),
            DateTime.Parse(historialcoordinador.Fechafin)));
    }
}