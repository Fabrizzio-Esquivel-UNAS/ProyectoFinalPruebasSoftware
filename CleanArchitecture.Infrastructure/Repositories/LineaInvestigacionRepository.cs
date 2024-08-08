using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class LineasInvestigacionRepository : BaseRepository<LineaInvestigacion>, ILineaInvestigacionRepository
{
    public LineasInvestigacionRepository(ApplicationDbContext context) : base(context)
    {
    }
}