using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Solicitud;

namespace CleanArchitecture.gRPC.Interfaces;

public interface ISolicitudesContext
{
    Task<IEnumerable<SolicitudViewModel>> GetSolicitudesByIds(IEnumerable<Guid> ids);
}