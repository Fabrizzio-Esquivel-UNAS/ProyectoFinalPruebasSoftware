using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Facultad;

namespace CleanArchitecture.gRPC.Interfaces;

public interface IFacultadesContext
{
    Task<IEnumerable<FacultadViewModel>> GetFacultadesByIds(IEnumerable<Guid> ids);
}