using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Contratos.UpdateContrato;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.User;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Users.UpdateUser;

public sealed class UpdateUserCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateUserCommand>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IHistorialCoordinadorRepository _historialCoordinadorRepository;

    public UpdateUserCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IUserRepository userRepository,
        IUser user,
        ITenantRepository tenantRepository,
        IContratoRepository contratoRepository,
        ISolicitudRepository solicitudRepository,
        IHistorialCoordinadorRepository historialCoordinadorRepository) : base(bus, unitOfWork, notifications)
    {
        _userRepository = userRepository;
        _user = user;
        _tenantRepository = tenantRepository;
        _contratoRepository = contratoRepository;
        _solicitudRepository = solicitudRepository;
        _historialCoordinadorRepository = historialCoordinadorRepository;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Id {request.UserId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        /*
        if (_user.GetUserId() != request.UserId && _user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to update user {request.UserId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }
        */

        if (request.Email != user.Email)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser is not null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is already a user with email {request.Email}",
                        DomainErrorCodes.User.AlreadyExists));
                return;
            }
        }

        //if (_user.GetUserRole() == UserRole.Admin)
        //{
            //user.Role = request.Role;

            //if (!await _tenantRepository.ExistsAsync(request.TenantId))
            //{
            //    await NotifyAsync(
            //        new DomainNotification(
            //            request.MessageType,
            //            $"There is no tenant with Id {request.TenantId}",
            //            ErrorCodes.ObjectNotFound));
            //    return;
            //}
            //user.TenantId = request.TenantId;
        //}

        user.Email = request.Email;
        user.EscuelaId = request.EscuelaId;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Telefono = request.Telefono;
        user.Telefono = request.Telefono;
        user.Notificaciones = request.Notificaciones;
        if (request.AsesoriaEstado!=null) {
            user.AsesoriaEstado = (AsesoriaEstado)request.AsesoriaEstado;
        }

        if (user.Role==UserRole.Asesorado && request.Role==UserRole.User)
        {
            var solicitud = await _solicitudRepository.GetLatestAceptadoSolicitudByAsesoradoUserIdAsync(request.UserId);
            if (solicitud != null)
            {
                var contrato = await _contratoRepository.GetBySolicitudIdAsync(solicitud.Id);
                if (contrato != null && contrato.FechaFinal==null)
                {
                    await Bus.SendCommandAsync(new UpdateContratosCommand(contrato.Id));
                }
            }
        } else if (user.Role==UserRole.Coordinador && request.Role!=UserRole.Coordinador) {
            var grupoInvestigacion = await _historialCoordinadorRepository.GetLatestActiveCoordinadorByUserIdAsync(request.UserId);
            if (grupoInvestigacion!=null)
            {
                await Bus.SendCommandAsync(new UpdateHistorialCoordinadorCommand(
                    grupoInvestigacion.Id,
                    DateTime.UtcNow));
            }
        }

        user.Role = request.Role;

        _userRepository.Update(user);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new UserUpdatedEvent(user.Id, user.TenantId));
        }
    }
}