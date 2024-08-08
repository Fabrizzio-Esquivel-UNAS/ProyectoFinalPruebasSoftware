using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.Solicitudes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class SolicitudesApiImplementation : SolicitudesApi.SolicitudesApiBase
{
    private readonly ISolicitudRepository _solicitudRepository;

    public SolicitudesApiImplementation(ISolicitudRepository solicitudRepository)
    {
        _solicitudRepository = solicitudRepository;
    }

    public override async Task<GetSolicitudesByIdsResult> GetByIds(
        GetSolicitudesByIdsRequest request,
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

        var solicitudes = await _solicitudRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(solicitud => idsAsGuids.Contains(solicitud.Id))
            .Select(solicitud => new Solicitud
            {
                Id = solicitud.Id.ToString(),
                NumeroTesis = solicitud.NumeroTesis,
                IsDeleted = solicitud.Deleted
            })
            .ToListAsync();

        var result = new GetSolicitudesByIdsResult();

        result.Solicitudes.AddRange(solicitudes);

        return result;
    }
}