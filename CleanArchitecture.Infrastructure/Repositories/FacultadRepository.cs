using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class FacultadesRepository : BaseRepository<Facultad>, IFacultadRepository
{
    public FacultadesRepository(ApplicationDbContext context) : base(context)
    {
    }
}