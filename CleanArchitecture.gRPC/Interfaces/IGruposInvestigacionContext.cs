using CleanArchitecture.Shared.GrupoInvestigacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.gRPC.Interfaces;
public interface IGruposInvestigacionContext
{
    Task<IEnumerable<GrupoInvestigacionViewModel>> GetGruposInvestigacionByIds(IEnumerable<Guid> ids);
}
