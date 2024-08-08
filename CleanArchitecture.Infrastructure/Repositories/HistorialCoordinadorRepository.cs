using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class HistorialCoordinadorRepository : BaseRepository<HistorialCoordinador>, IHistorialCoordinadorRepository
{
    public HistorialCoordinadorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<HistorialCoordinador?> GetLatestActiveCoordinadorByGrupoInvestigacionIdAsync(Guid GrupoInvestigacionId)
    {
        return await DbSet.Where(h => h.FechaFin == null && h.GrupoInvestigacionId == GrupoInvestigacionId)
            .OrderByDescending(h => h.FechaInicio)
            .FirstOrDefaultAsync();
    }

    public async Task<HistorialCoordinador?> GetLatestActiveCoordinadorByUserIdAsync(Guid UserId)
    {
        return await DbSet.Where(h => h.FechaFin == null && h.UserId == UserId)
            .OrderByDescending(h => h.FechaInicio)
            .FirstOrDefaultAsync();
    }
}