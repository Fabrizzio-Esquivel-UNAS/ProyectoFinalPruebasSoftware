using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class CalendarioRepository : BaseRepository<Calendario>, ICalendarioRepository
{
    public CalendarioRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<Calendario?> GetByUserIdAsync(Guid userId)
    {
        return await DbSet.SingleOrDefaultAsync(Calendario => Calendario.UserId == userId);
    }
}