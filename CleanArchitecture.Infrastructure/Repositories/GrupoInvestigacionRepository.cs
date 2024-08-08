using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Repositories;
public sealed class GrupoInvestigacionRepository : BaseRepository<GrupoInvestigacion>, IGrupoInvestigacionRepository
{
    public GrupoInvestigacionRepository(ApplicationDbContext context) : base(context)
    {
    }
}

