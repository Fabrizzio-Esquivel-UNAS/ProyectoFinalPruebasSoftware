using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class EscuelasRepository : BaseRepository<Escuela>, IEscuelaRepository
{
    public EscuelasRepository(ApplicationDbContext context) : base(context)
    {
    }
}