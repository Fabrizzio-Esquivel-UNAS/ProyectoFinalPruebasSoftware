using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Users.UpdateUser;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Shared.Events.Cita;
using CleanArchitecture.Shared.Events.User;
using MediatR;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Domain.Commands.Citas.UpdateCita;

public sealed class UpdateCitaCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateCitaCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IOptionsSnapshot<AsesoriaSettings> _asesoriaSettings;
    private readonly ICitaRepository _citaRepository;
    private readonly IUser _user;

    public UpdateCitaCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICitaRepository citaRepository,
        ISolicitudRepository solicitudRepository,
        IContratoRepository contratoRepository,
        IUserRepository userRepository,
        IOptionsSnapshot<AsesoriaSettings> asesoriaSettings,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _userRepository = userRepository;
        _contratoRepository = contratoRepository;
        _solicitudRepository = solicitudRepository;
        _asesoriaSettings = asesoriaSettings;
        _citaRepository = citaRepository;
        _user = user;

    }

    public async Task Handle(UpdateCitaCommand request, CancellationToken cancellationToken)
    {
        
        if (!await TestValidityAsync(request))
        {
            return;
        }

        /*
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to update cita {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }
        */

        var cita = await _citaRepository.GetByIdAsync(request.AggregateId);

        if (cita is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no cita with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        cita.Estado = request.Estado;
        cita.DesarrolloAsesor = request.DesarrolloAsesor;
        cita.DesarrolloAsesorado = request.DesarrolloAsesorado;

        var solicitud = await _solicitudRepository.GetLatestAceptadoSolicitudByAsesoradoUserIdAsync(cita.AsesoradoUserId);
        if (solicitud != null)
        {
            var contrato = await _contratoRepository.GetBySolicitudIdAsync(solicitud.Id);
            if (contrato != null && contrato.FechaFinal == null)
            {
                int inasistencias = await _citaRepository.GetNumberOfInasistenciasAsync(cita.AsesoradoUserId, contrato.FechaInicio);
                var user = await _userRepository.GetByIdAsync(cita.AsesoradoUserId);
                var estadoAsesoria = 
                    inasistencias > _asesoriaSettings.Value.FaltasRequeridasParaAbandonoDeAsesoria ? AsesoriaEstado.Abandonado
                    : inasistencias > _asesoriaSettings.Value.FaltasRequeridasParaRiesgoDeAsesoria ? AsesoriaEstado.Riesgo
                    : AsesoriaEstado.Normal;
                await Bus.SendCommandAsync(new UpdateUserCommand(
                    user!.Id,
                    user.EscuelaId,
                    user.Email,
                    user.FirstName,
                    user.FirstName,
                    user.Telefono,
                    user.Notificaciones,
                    estadoAsesoria == AsesoriaEstado.Abandonado ? UserRole.User : user.Role,
                    estadoAsesoria));
            }
        }

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CitaUpdatedEvent(
                cita.Id,
                cita.EventoId,
                cita.AsesorUserId,
                cita.AsesoradoUserId));
        }
    }
}