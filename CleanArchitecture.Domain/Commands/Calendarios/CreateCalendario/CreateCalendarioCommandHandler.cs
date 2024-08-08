using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.DTOs.Calendly;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Shared.Events.Calendario;
using CleanArchitecture.Shared.Events.User;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;

namespace CleanArchitecture.Domain.Commands.Calendarios.CreateCalendario;

public sealed class CreateCalendarioCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateCalendarioCommand>
{
    private readonly ICalendly _calendly;
    private readonly ICalendarioRepository _calendarioRepository;
    private readonly IUser _user;
    private readonly CalendlySettings _calendlySettings;

    public CreateCalendarioCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICalendly calendly,
        ICalendarioRepository calendarioRepository,
        IOptions<CalendlySettings> calendlySettings,
        IUser user
        ) : base(bus, unitOfWork, notifications)    {
        _calendly = calendly;
        _calendarioRepository = calendarioRepository;
        _calendlySettings = calendlySettings.Value;
        _user = user;
    }

    public async Task Handle(CreateCalendarioCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
        /*
        if (_user.GetUserRole() != UserRole.Asesor)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to create calendario {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

        if (await _calendarioRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a calendario with Id {request.AggregateId}",
                    DomainErrorCodes.Calendario.AlreadyExists));

            return;
        }

        // Determinar la Id del Usuario asociado al calendario
        var userId = _user.GetUserId();
        var existingCalendario = await _calendarioRepository.GetByUserIdAsync(userId);
        if (existingCalendario is not null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a calendario with userId {userId}",
                    DomainErrorCodes.Calendario.AlreadyExists));
            return;
        }

        try
        {
            CalendlyUserResponse userResponse = await _calendly.GetDataAsync<CalendlyUserResponse>(_calendlySettings.UserEndpoint, request.AccessToken);

            var calendario = new Calendario(
                request.AggregateId,
                userId,
                userResponse.Resource.Uri,
                request.AccessToken,
                request.RefreshToken,
                request.AccessTokenExpiration,
                request.RefreshTokenExpiration);

            _calendarioRepository.Add(calendario);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CalendarioCreatedEvent(
                    calendario.UserId,
                    calendario.Id,
                    calendario.AccessToken,
                    calendario.RefreshToken,
                    calendario.UserUri));
            }
        }
        catch (HttpRequestException httpEx)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                "There was an error connecting to the Calendly service. Please try again later.",
                ErrorCodes.HttpRequestFailed,
                httpEx));
        }
        catch (InvalidOperationException invalidOpEx)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                invalidOpEx.Message,
                ErrorCodes.InvalidOperation,
                invalidOpEx));
        }
        catch (Exception ex)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                "An unexpected error occurred. Please try again later.",
                ErrorCodes.UnexpectedError,
                ex));
        }
    }
}