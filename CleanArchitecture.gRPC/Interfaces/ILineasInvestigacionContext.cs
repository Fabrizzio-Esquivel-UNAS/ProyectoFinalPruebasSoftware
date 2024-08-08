using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.LineaInvestigacion;

namespace CleanArchitecture.gRPC.Interfaces;

public interface ILineasInvestigacionContext
{
    Task<IEnumerable<LineaInvestigacionViewModel>> GetLineasInvestigacionByIds(IEnumerable<Guid> ids);
}