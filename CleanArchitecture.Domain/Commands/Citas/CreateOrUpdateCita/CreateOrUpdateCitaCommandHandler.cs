using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Contratos.UpdateContrato;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Cita;
using Google.Apis.Calendar.v3.Data;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Citas.CreateCita;

public sealed class CreateOrUpdateCitaCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateOrUpdateCitaCommand>
{
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly ICitaRepository _citaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUser _user;

    public CreateOrUpdateCitaCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICitaRepository citaRepository,
        IUserRepository userRepository,
        ISolicitudRepository solicitudRepository,
        IContratoRepository contratoRepository,
        IUser user
        ) : base(bus, unitOfWork, notifications)
    {
        _solicitudRepository = solicitudRepository;
        _contratoRepository = contratoRepository;
        _citaRepository = citaRepository;
        _userRepository = userRepository;
        _user = user;
    }

    private async Task Create(CreateOrUpdateCitaCommand request, CancellationToken cancellationToken)
    {
        if (await _citaRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a cita with Id {request.AggregateId}",
                    DomainErrorCodes.Cita.AlreadyExists));
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

        var asesoradoUser = await _userRepository.GetByEmailAsync(request.AsesoradoEmail);
        if (asesoradoUser is null)
        {
            /*
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Email {request.AsesoradoEmail}",
                    ErrorCodes.ObjectNotFound));
            */
            WarningException myEx = new WarningException($"There is no user with Email {request.AsesoradoEmail}");
            Console.Write(myEx.ToString());
            return;
        }

        var solicitud = await _solicitudRepository.GetLatestAceptadoSolicitudByAsesoradoUserIdAsync(asesoradoUser.Id);
        var citaFecha = DateOnly.FromDateTime(request.FechaCreacion);
        var contratoRange = false;
        if (solicitud != null)
        {
            var contrato = await _contratoRepository.GetBySolicitudIdAsync(solicitud.Id);
            if (contrato!=null 
                && citaFecha>=contrato.FechaInicio 
                && (contrato.FechaFinal==null || citaFecha<=contrato.FechaFinal))
            {
                contratoRange = true;
            }
        }
        if (contratoRange==false) {
            WarningException myEx = new WarningException($"The User with Id {asesoradoUser.Id} does not have any Contrato at date {citaFecha}");
            Console.Write(myEx.ToString());
            return;
        }

        var cita = new Cita(
            request.AggregateId,
            request.EventoId,
            request.AsesorUserId,
            asesoradoUser.Id,
            request.FechaCreacion,
            request.FechaInicio,
            request.FechaFin,
            request.Estado);

        _citaRepository.Add(cita);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CitaCreatedEvent(
                cita.Id,
                cita.EventoId,
                cita.AsesorUserId,
                cita.AsesoradoUserId));
        }
    }

    private async Task Update(CreateOrUpdateCitaCommand request, CancellationToken cancellationToken, Cita cita)
    {
        cita.Estado = request.Estado;
        //cita.DesarrolloAsesor = cita.DesarrolloAsesor;
        //cita.DesarrolloAsesorado = cita.DesarrolloAsesorado;

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CitaUpdatedEvent(
                cita.Id,
                cita.EventoId,
                cita.AsesorUserId,
                cita.AsesoradoUserId));
        }
    }
    public async Task Handle(CreateOrUpdateCitaCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create cita {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }
        */

        var existingCita = await _citaRepository.GetByEventoIdAsync(request.EventoId);
        if (existingCita is null)
        {
            await Create(request, cancellationToken);
        }
        else
        {
            await Update(request, cancellationToken, existingCita);
        }
    }
}