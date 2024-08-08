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

namespace CleanArchitecture.Application.Queries.Calendarios.GetCalendarioById;

public sealed class GetCalendarioByIdQueryHandler :
    IRequestHandler<GetCalendarioByIdQuery, CalendarioViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICalendarioRepository _calendarioRepository;
    private readonly ICalendly _calendly;
    private readonly CalendlySettings _calendlySettings;

    public GetCalendarioByIdQueryHandler(ICalendarioRepository calendarioRepository, IMediatorHandler bus, ICalendly calendly, IOptions<CalendlySettings> calendlySettings)
    {
        _calendarioRepository = calendarioRepository;
        _bus = bus;
        _calendly = calendly;
        _calendlySettings = calendlySettings.Value;
    }

    public async Task<CalendarioViewModel?> Handle(GetCalendarioByIdQuery request, CancellationToken cancellationToken)
    {
        var calendario = await _calendarioRepository.GetByIdAsync(request.CalendarioId);

        if (calendario is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCalendarioByIdQuery),
                    $"Calendario with id {request.CalendarioId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CalendarioViewModel.FromCalendario(calendario);
    }
}