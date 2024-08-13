using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Shared.Events.Solicitud;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;

public sealed class CreateSolicitudCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateSolicitudCommand>
{
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOptionsSnapshot<AsesoriaSettings> _asesoriaSettings;
    private readonly IUserNotifications _userNotifications;
    private readonly IUser _user;

    public CreateSolicitudCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISolicitudRepository solicitudRepository,
        IUserRepository userRepository,
        IOptionsSnapshot<AsesoriaSettings> asesoriaSettings,
        IUserNotifications userNotifications,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _solicitudRepository = solicitudRepository;
        _userRepository = userRepository;
        _asesoriaSettings = asesoriaSettings;
        _userNotifications = userNotifications;
        _user = user;
    }

    public async Task Handle(CreateSolicitudCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.User)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to create solicitud {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        if (await _solicitudRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a solicitud with Id {request.AggregateId}",
                    DomainErrorCodes.Solicitud.AlreadyExists));

            return;
        }

        var asesorUser = await _userRepository.GetByIdAsync(request.AsesorUserId);
        if (asesorUser is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Id {request.AsesorUserId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }
        if (asesorUser.Role > UserRole.Asesor)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"The user with Id {request.AsesorUserId} does not have the permissions of 'Asesor' role",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var asesoradoUser = await _userRepository.GetByIdAsync(request.AsesoradoUserId);
        if (asesoradoUser is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Id {request.AsesoradoUserId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }
        if (asesoradoUser.Role != UserRole.User)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"The user with Id {asesoradoUser.Id} does not have the required role of 'User'",
                    DomainErrorCodes.Cita.UserMissingAsesoradoRole));
            return;
        }

        var solicitud = new Solicitud(
            request.SolicitudId,
            request.AsesoradoUserId,
            request.AsesorUserId,
            request.NumeroTesis,
            request.Mensaje,
            SolicitudStatus.Pendiente);

        _solicitudRepository.Add(solicitud);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new SolicitudCreatedEvent(
                solicitud.Id,
                solicitud.AsesoradoUserId,
                solicitud.AsesorUserId,
                solicitud.NumeroTesis,
                solicitud.Mensaje));
            _userNotifications.NotifyUser(asesorUser, "NUEVA SOLICITUD", "Nueva solicitud de "+asesoradoUser.FullName);
        }
    }
}