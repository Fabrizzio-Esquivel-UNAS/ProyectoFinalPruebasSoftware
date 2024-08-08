using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Contratos;

namespace CleanArchitecture.gRPC.Interfaces;

public interface IContratosContext
{
    Task<IEnumerable<ContratoViewModel>> GetContratosByIds(IEnumerable<Guid> ids);
}