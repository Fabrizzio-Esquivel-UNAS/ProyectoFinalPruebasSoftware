using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class SolicitudRepository : BaseRepository<Solicitud>, ISolicitudRepository
{
    public SolicitudRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IEnumerable<Solicitud> GetByFechaCreacionAsync(DateTime start, DateTime end)
    {
        return DbSet
            .Where(Cita => Cita.FechaCreacion >= start && Cita.FechaCreacion <= end)
            .ToList();
    }

    public async Task<Solicitud?> GetLatestAceptadoSolicitudByAsesoradoUserIdAsync(Guid asesoradoUserId)
    {
        return await DbSet.Where(s => s.Estado==SolicitudStatus.Aceptado && s.AsesoradoUserId==asesoradoUserId)
            .OrderByDescending(s => s.FechaRespuesta)
            .FirstOrDefaultAsync();
    }
}