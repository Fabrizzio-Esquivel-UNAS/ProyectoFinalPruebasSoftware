using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain.Commands.Calendarios.UpdateCalendario;
using CleanArchitecture.Domain.DTOs.Calendly;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CleanArchitecture.Application.Queries.Calendarios.GetCalendarioByUserId;

public sealed class GetCalendarioByUserIdQueryHandler :
    IRequestHandler<GetCalendarioByUserIdQuery, CalendarioViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICalendarioRepository _calendarioRepository;
    private readonly ICalendly _calendly;
    private readonly CalendlySettings _calendlySettings;

    public GetCalendarioByUserIdQueryHandler(ICalendarioRepository calendarioRepository, IMediatorHandler bus, ICalendly calendly, IOptions<CalendlySettings> calendlySettings)
    {
        _calendarioRepository = calendarioRepository;
        _bus = bus;
        _calendly = calendly;
        _calendlySettings = calendlySettings.Value;
    }

    public async Task<CalendarioViewModel?> Handle(GetCalendarioByUserIdQuery request, CancellationToken cancellationToken)
    {
        var calendario = await _calendarioRepository.GetByUserIdAsync(request.UserId);

        if (calendario is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCalendarioByUserIdQuery),
                    $"Calendario with user id {request.UserId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        //// Retrieve user tokens from the database
        //if (string.IsNullOrEmpty(calendario.RefreshToken))
        //    throw new Exception("No refresh token found.");

        //// Check if the access token is still valid
        //if (calendario.AccessTokenExpiration > DateTime.UtcNow)
        //    return CalendarioViewModel.FromCalendario(calendario);

        //// Token is expired, refresh it
        //CalendlyToken newTokens = await _calendly.GetTokensWithRefreshToken(calendario.RefreshToken);

        //// Update the database with the new tokens
        //await _bus.SendCommandAsync(new UpdateCalendarioCommand(
        //    calendario.Id,
        //    calendario.UserUri,
        //    newTokens.AccessToken,
        //    newTokens.RefreshToken,
        //    DateTime.UtcNow.AddSeconds(newTokens.ExpiresIn),
        //    calendario.RefreshTokenExpiration,
        //    calendario.EventType,
        //    calendario.EventsPageToken,
        //    calendario.SchedulingUrl));

        return CalendarioViewModel.FromCalendario(calendario);
    }
}