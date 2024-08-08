using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories;
public interface IHistorialCoordinadorRepository : IRepository<HistorialCoordinador>
{
    Task<HistorialCoordinador?> GetLatestActiveCoordinadorByUserIdAsync(Guid UserId);
    Task<HistorialCoordinador?> GetLatestActiveCoordinadorByGrupoInvestigacionIdAsync(Guid GrupoInvestigacionId);
}
