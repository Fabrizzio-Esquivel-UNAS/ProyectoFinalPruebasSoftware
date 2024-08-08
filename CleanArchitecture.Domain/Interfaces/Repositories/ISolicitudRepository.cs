using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories;

public interface ISolicitudRepository : IRepository<Solicitud>
{
    IEnumerable<Solicitud> GetByFechaCreacionAsync(DateTime start, DateTime end);
    Task<Solicitud?> GetLatestAceptadoSolicitudByAsesoradoUserIdAsync(Guid AsesoradoUserId);
}