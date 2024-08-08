using CleanArchitecture.Shared.HistorialCoordinador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.gRPC.Interfaces;
public interface IHistorialCoordinadoresContext
{
    Task<IEnumerable<HistorialCoordinadorViewModel>> GetHistorialCoordinadoresByIds(IEnumerable<Guid> ids);
}
