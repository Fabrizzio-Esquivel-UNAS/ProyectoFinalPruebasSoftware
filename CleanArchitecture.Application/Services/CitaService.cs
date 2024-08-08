using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Citas.GetAll;
using CleanArchitecture.Application.Queries.Citas.GetCitaById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Citas;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Commands.Citas.DeleteCita;
using CleanArchitecture.Domain.Commands.Citas.UpdateCita;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using CleanArchitecture.Domain.Settings;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace CleanArchitecture.Application.Services;

public sealed class CitaService : ICitaService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;


    public CitaService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateOrUpdateCitaAsync(CreateCitaViewModel cita)
    {
        var citaId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateOrUpdateCitaCommand(
            citaId,
            cita.EventoId,
            cita.AsesorUserId,
            cita.AsesoradoEmail,
            cita.FechaCreacion,
            cita.FechaInicio,
            cita.FechaFin,
            cita.Estado));

        return citaId;
    }

    public async Task UpdateCitaAsync(UpdateCitaViewModel cita)
    {
        await _bus.SendCommandAsync(new UpdateCitaCommand(
            cita.Id,
            cita.Estado,
            cita.DesarrolloAsesor,
            cita.DesarrolloAsesorado));
    }

    public async Task DeleteCitaAsync(Guid citaId)
    {
        await _bus.SendCommandAsync(new DeleteCitaCommand(citaId));
    }

    public async Task<CitaViewModel?> GetCitaByIdAsync(Guid citaId)
    {
        var cachedCita = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Cita>(citaId),
            async () => await _bus.QueryAsync(new GetCitaByIdQuery(citaId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedCita;
    }

    public async Task<PagedResult<CitaViewModel>> GetAllCitasAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllCitasQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}