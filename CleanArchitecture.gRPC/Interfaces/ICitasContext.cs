using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Citas;

namespace CleanArchitecture.gRPC.Interfaces;

public interface ICitasContext
{
    Task<IEnumerable<CitaViewModel>> GetCitasByIds(IEnumerable<Guid> ids);
}