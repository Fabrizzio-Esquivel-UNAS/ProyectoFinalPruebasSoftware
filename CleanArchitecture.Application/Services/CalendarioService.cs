using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Calendarios.GetAll;
using CleanArchitecture.Application.Queries.Calendarios.GetCalendarioById;
using CleanArchitecture.Application.Queries.Calendarios.GetAccessToken;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Calendarios.CreateCalendario;
using CleanArchitecture.Domain.Commands.Calendarios.DeleteCalendario;
using CleanArchitecture.Domain.Commands.Calendarios.UpdateCalendario;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using CleanArchitecture.Domain.Constants;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Polly;
using CleanArchitecture.Application.ViewModels.Citas;
using Microsoft.IdentityModel.Tokens;
using CleanArchitecture.Domain.DTOs.Calendly;
using System.Linq;
using CleanArchitecture.Domain.Enums;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Application.Queries.Calendarios.GetCalendarioByUserId;

namespace CleanArchitecture.Application.Services
{
    public sealed class CalendarioService : ICalendarioService
    {
        private readonly IMediatorHandler _bus;
        private readonly IDistributedCache _distributedCache;
        private readonly IUserService _userService;
        private readonly ICitaService _citaService;
        private readonly ICalendly _calendly;
        private readonly IConfiguration _configuration;
        private readonly CalendlySettings _calendlySettings;

        // Constructor to initialize dependencies
        public CalendarioService(IMediatorHandler bus, IDistributedCache distributedCache, IUserService userService, ICitaService citaService, ICalendly calendly, IOptions<CalendlySettings> calendlySettings, IConfiguration configuration)
        {
            _bus = bus;
            _distributedCache = distributedCache;
            _userService = userService;
            _citaService = citaService;
            _calendly = calendly;
            _calendlySettings = calendlySettings.Value;
            _configuration = configuration;
        }

        // Method to create a new calendar
        public async Task<Guid> CreateCalendarioAsync(CreateCalendarioViewModel calendario)
        {
            var calendarioId = Guid.NewGuid(); // Generate new GUID for calendario
            DateTime? accessTokenExpiration = calendario.AccessTokenExpiration==null ? null : DateTime.UtcNow.AddSeconds((double)calendario.AccessTokenExpiration);
            DateTime? refreshTokenExpiration = calendario.RefreshTokenExpiration == null ? null : DateTime.UtcNow.AddSeconds((double)calendario.RefreshTokenExpiration);
            await _bus.SendCommandAsync(new CreateCalendarioCommand(
                calendarioId,
                calendario.AccessToken,
                calendario.RefreshToken,
                accessTokenExpiration,
                refreshTokenExpiration));

            return calendarioId; // Return the new calendario ID
        }

        // Method to update an existing calendar
        public async Task UpdateCalendarioAsync(UpdateCalendarioViewModel calendario)
        {
            await _bus.SendCommandAsync(new UpdateCalendarioCommand(
                calendario.Id,
                calendario.UserUri,
                calendario.AccessToken,
                calendario.RefreshToken,
                calendario.AccessTokenExpiration,
                calendario.RefreshTokenExpiration,
                calendario.EventType,
                calendario.EventsPageToken, 
                calendario.SchedulingUrl));
        }

        // Method to delete a calendar by ID
        public async Task DeleteCalendarioAsync(Guid calendarioId)
        {
            await _bus.SendCommandAsync(new DeleteCalendarioCommand(calendarioId));
        }

        // Method to synchronize calendar events from the Calendly API
        public async Task SincronizarCalendarioAsync(Guid calendarioId)
        {
            var calendario = await GetCalendarioByIdAsync(calendarioId)
                ?? throw new InvalidOperationException("Calendario could not be found.");

            using (var httpClient = _calendly.GetHttpClient(calendario.AccessToken))
            {
                var pageToken = calendario.EventsPageToken;
                do
                {
                    CalendlyScheduledEventResponse eventsResponse = await _calendly.GetScheduledEvents(calendario.UserUri, httpClient, pageToken);
                    foreach (CalendlyScheduledEvent calendlyEvent in eventsResponse.Collection)
                    {
                        if (calendlyEvent.EventType == calendario.EventType)
                        {
                            CalendlyInvitee invitee = await _calendly.GetInvitee(calendlyEvent.Uri, httpClient);
                            CitaEstado citaEstado = 
                                DateTime.UtcNow > DateTime.Parse(calendlyEvent.EndTime) ? CitaEstado.Completado
                                : (calendlyEvent.Status == "canceled") ? CitaEstado.Cancelado
                                : CitaEstado.Programado;
                            await _citaService.CreateOrUpdateCitaAsync(new CreateCitaViewModel(
                                calendlyEvent.Uri,
                                calendario.UserId,
                                invitee.Email,
                                DateTime.Parse(calendlyEvent.CreatedAt),
                                DateTime.Parse(calendlyEvent.StartTime),
                                DateTime.Parse(calendlyEvent.EndTime),
                                citaEstado));
                        }
                    }
                    if (string.IsNullOrEmpty(eventsResponse.Pagination.NextPage))
                        break;
                    pageToken = eventsResponse.Pagination.NextPageToken;
                } while (true);

                if (pageToken != null)
                {
                    await UpdateCalendarioAsync(new UpdateCalendarioViewModel(
                        calendarioId,
                        calendario.UserUri,
                        calendario.AccessToken,
                        calendario.RefreshToken,
                        calendario.AccessTokenExpiration,
                        calendario.RefreshTokenExpiration,
                        calendario.EventType,
                        pageToken,
                        calendario.SchedulingUrl));
                }
            }
        }

        // Method to link a calendar to an event type on Calendly
        public async Task<CalendarioViewModel> VincularCalendarioAsync(Guid calendarioId, string calendarioNombre)
        {
            string calendarioEventType = string.Empty;
            string calendarioSchedulingUrl = string.Empty;
            var calendario = await GetCalendarioByIdAsync(calendarioId)
                ?? throw new InvalidOperationException("Calendario could not be found.");

            CalendlyEventTypeResponse eventTypeResponse = await _calendly.GetDataAsync<CalendlyEventTypeResponse>(
                String.Format(_calendlySettings.EventTypesEndpoint, calendario.UserUri), 
                calendario.AccessToken);
            foreach (CalendlyEventType eventType in eventTypeResponse.Collection)
            {
                if (eventType.Name == calendarioNombre)
                {
                    calendarioEventType = eventType.Uri;
                    calendarioSchedulingUrl = eventType.Scheduling_Url;
                    break;
                }
            }
            // Ensure the event type was found
            if (string.IsNullOrEmpty(calendarioEventType))
            {
                throw new InvalidOperationException($"Event type '{calendarioNombre}' could not be found.");
            }
            await UpdateCalendarioAsync(new UpdateCalendarioViewModel(
                calendarioId,
                calendario.UserUri,
                calendario.AccessToken,
                calendario.RefreshToken,
                calendario.AccessTokenExpiration,
                calendario.RefreshTokenExpiration,
                calendarioEventType,
                calendario.EventsPageToken,
                calendarioSchedulingUrl));

            // Remover Cache
            await _distributedCache.RemoveAsync(CacheKeyGenerator.GetEntityCacheKey<Calendario>(calendarioId));

            return new CalendarioViewModel
            {
                Id = calendario.Id,
                UserUri = calendario.UserUri,
                AccessToken = calendario.AccessToken,
                RefreshToken = calendario.RefreshToken,
                AccessTokenExpiration = calendario.AccessTokenExpiration,
                RefreshTokenExpiration = calendario.RefreshTokenExpiration,
                EventType = calendarioEventType,
                EventsPageToken = calendario.EventsPageToken,
                SchedulingUrl = calendarioSchedulingUrl,
            };
        }

        // Method to get all calendars with pagination, sorting, and optional search term
        public async Task<PagedResult<CalendarioViewModel>> GetAllCalendariosAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCalendariosQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        // Method to get a specific calendar by its ID, with caching
        public async Task<CalendarioViewModel?> GetCalendarioByIdAsync(Guid calendarioId)
        {
            var cacheKey = CacheKeyGenerator.GetEntityCacheKey<Calendario>(calendarioId);
            var cachedCalendario = await _distributedCache.GetOrCreateJsonAsync(
                cacheKey,
                async () => await _bus.QueryAsync(new GetCalendarioByIdQuery(calendarioId)),
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(3),
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
                });

            return await CheckRefreshTokens(cachedCalendario, cacheKey);
        }

        // Method to get a specific calendar by its user ID, with caching
        public async Task<CalendarioViewModel?> GetCalendarioByUserIdAsync(Guid calendarioUserId)
        {
            var cacheKey = CacheKeyGenerator.GetEntityCacheKey<Calendario>(calendarioUserId);
            var cachedCalendario = await _distributedCache.GetOrCreateJsonAsync(
                cacheKey,
                async () => await _bus.QueryAsync(new GetCalendarioByUserIdQuery(calendarioUserId)),
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(3),
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
                });

            return await CheckRefreshTokens(cachedCalendario, cacheKey);
        }

        // Method to get the access token for a specific user, with caching
        public async Task<string?> GetAccessTokenAsync(Guid userId)
        {
            var cachedCalendario = await _distributedCache.GetOrCreateJsonAsync(
                CacheKeyGenerator.GetEntityCacheKey<Calendario>(userId),
                async () => await _bus.QueryAsync(new GetAccessTokenQuery(userId)),
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(3),
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
                });

            if (cachedCalendario != null)
            {
                return cachedCalendario.AccessToken;
            }
            return null;
        }

        public string AuthCalendarioAsync(string redirectUri)
        {
            return _calendly.GetAuthCode(redirectUri);
        }

        public async Task<CalendlyToken> GetTokensAsync(string code)
        {
            return await _calendly.GetTokensWithAuthCode(code);
        }

        private async Task<CalendarioViewModel?> CheckRefreshTokens(CalendarioViewModel? calendario, string? cacheKey)
        {
            // Validate AccessToken
            if (calendario == null 
                || calendario.AccessToken == null 
                || calendario.RefreshToken == null 
                || calendario.AccessTokenExpiration == null 
                || calendario.AccessTokenExpiration > DateTime.UtcNow)
                return calendario!;

            // Remover Cache
            if (cacheKey != null)
            {
                await _distributedCache.RemoveAsync(cacheKey);
            }

            // Token is expired, refresh it
            CalendlyToken newTokens = await _calendly.GetTokensWithRefreshToken(calendario.RefreshToken);

            // Update ViewModel with new tokens
            calendario.RefreshToken = newTokens.RefreshToken;
            calendario.AccessToken = newTokens.AccessToken;
            calendario.AccessTokenExpiration = DateTime.UtcNow.AddSeconds(newTokens.ExpiresIn);

            // Update Calendar in database with new tokens
            await UpdateCalendarioAsync(new UpdateCalendarioViewModel(
                calendario.Id,
                calendario.UserUri,
                calendario.AccessToken,
                calendario.RefreshToken,
                calendario.AccessTokenExpiration,
                calendario.RefreshTokenExpiration,
                calendario.EventType,
                calendario.EventsPageToken,
                calendario.SchedulingUrl));

            return calendario;
        }
    }
}