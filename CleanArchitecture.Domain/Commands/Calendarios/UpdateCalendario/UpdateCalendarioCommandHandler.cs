using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Calendario;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Calendarios.UpdateCalendario;

public sealed class UpdateCalendarioCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateCalendarioCommand>
{
    private readonly ICalendarioRepository _calendarioRepository;
    private readonly IUser _user;

    public UpdateCalendarioCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICalendarioRepository calendarioRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _calendarioRepository = calendarioRepository;
        _user = user;
    }

    public async Task Handle(UpdateCalendarioCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var calendario = await _calendarioRepository.GetByIdAsync(request.AggregateId);

        if (calendario is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no calendario with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        calendario.AccessToken = request.AccessToken;
        calendario.AccessTokenExpiration = request.AccessTokenExpiration;
        calendario.RefreshToken = request.RefreshToken;
        calendario.RefreshTokenExpiration = request.RefreshTokenExpiration;
        calendario.UserUri = request.UserUri;
        calendario.EventType = request.EventType;
        calendario.EventsPageToken = request.EventsPageToken;
        calendario.SchedulingUrl = request.SchedulingUrl;

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CalendarioUpdatedEvent(
                calendario.Id,
                calendario.UserUri,
                calendario.AccessToken,
                calendario.RefreshToken,
                calendario.AccessTokenExpiration,
                calendario.RefreshTokenExpiration,
                calendario.EventType,
                calendario.EventsPageToken));
        }
    }
}