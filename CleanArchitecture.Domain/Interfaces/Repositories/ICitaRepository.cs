using CleanArchitecture.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories;

public interface ICitaRepository : IRepository<Cita>
{
    Task<Cita?> GetByEventoIdAsync(string eventoId);
    Task<int> GetNumberOfInasistenciasAsync(Guid AsesoradoUserId, DateOnly FechaInicio);
}