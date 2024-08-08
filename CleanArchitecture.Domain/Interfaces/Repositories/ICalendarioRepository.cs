using CleanArchitecture.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories;

public interface ICalendarioRepository : IRepository<Calendario>
{
    Task<Calendario?> GetByUserIdAsync(Guid userId);
}