using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class ContratoRepository : BaseRepository<Contrato>, IContratoRepository
{
    public ContratoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Contrato?> GetBySolicitudIdAsync(Guid solicitudId)
    {
        return await DbSet.SingleOrDefaultAsync(contrato => contrato.SolicitudId == solicitudId);
    }
}