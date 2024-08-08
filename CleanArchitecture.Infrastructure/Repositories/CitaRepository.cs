using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class CitaRepository : BaseRepository<Cita>, ICitaRepository
{
    public CitaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Cita?> GetByEventoIdAsync(string eventoId)
    {
        return await DbSet.SingleOrDefaultAsync(Cita => Cita.EventoId == eventoId);
    }

    public async Task<int> GetNumberOfInasistenciasAsync(Guid AsesoradoUserId, DateOnly FechaInicio)
    {
        return await DbSet
            .Where(c => c.AsesoradoUserId == AsesoradoUserId
                && DateOnly.FromDateTime(c.FechaCreacion) >= FechaInicio 
                && c.Estado == Domain.Enums.CitaEstado.Inasistido)
            .CountAsync();
    }
}