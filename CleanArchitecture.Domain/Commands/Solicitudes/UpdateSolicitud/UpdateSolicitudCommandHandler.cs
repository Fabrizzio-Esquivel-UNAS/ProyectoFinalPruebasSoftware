using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Contratos.CreateContrato;
using CleanArchitecture.Domain.Commands.Users.UpdateUser;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Solicitud;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;

public sealed class UpdateSolicitudCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateSolicitudCommand>
{
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IUser _user;

    public UpdateSolicitudCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISolicitudRepository solicitudRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _solicitudRepository = solicitudRepository;
        _user = user;
    }

    public async Task Handle(UpdateSolicitudCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() > UserRole.Asesor)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to update solicitud {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var solicitud = await _solicitudRepository.GetByIdAsync(request.AggregateId);

        if (solicitud is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no solicitud with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        if (solicitud.Estado == SolicitudStatus.Pendiente && request.Estado == SolicitudStatus.Aceptado)
        {
            await Bus.SendCommandAsync(new CreateContratoCommand(
                Guid.NewGuid(),
                solicitud.Id));

            var asesorado = solicitud.AsesoradoUser;
            if (asesorado.Role > UserRole.Asesorado)
            {
                await Bus.SendCommandAsync(new UpdateUserCommand(
                    asesorado.Id,
                    asesorado.EscuelaId,
                    asesorado.Email,
                    asesorado.FirstName,
                    asesorado.LastName,
                    asesorado.Telefono,
                    asesorado.Notificaciones,
                    UserRole.Asesorado,
                    AsesoriaEstado.Normal));
            }
        }

        solicitud.SetEstado(request.Estado);
        solicitud.SetFechaRespuesta(DateTimeOffset.UtcNow);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new SolicitudUpdatedEvent(
                solicitud.Id,
                (int)solicitud.Estado));
        }
    }
}