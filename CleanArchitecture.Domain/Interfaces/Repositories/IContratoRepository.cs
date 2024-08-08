using CleanArchitecture.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace CleanArchitecture.Domain.Interfaces.Repositories;

public interface IContratoRepository : IRepository<Contrato>
{
    Task<Contrato?> GetBySolicitudIdAsync(Guid solicitudId);
}