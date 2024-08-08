using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Escuela;

namespace CleanArchitecture.gRPC.Interfaces;

public interface IEscuelasContext
{
    Task<IEnumerable<EscuelaViewModel>> GetEscuelasByIds(IEnumerable<Guid> ids);
}