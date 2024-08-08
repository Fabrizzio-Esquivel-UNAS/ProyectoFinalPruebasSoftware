using CleanArchitecture.Domain.Commands.GruposInvestigacion.UpdateGrupoInvestigacion;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.GrupoInvestigacion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;

namespace CleanArchitecture.Domain.Commands.GruposInvestigacion.UpdateGrupoInvestigacion;
public sealed class UpdateGrupoInvestigacionCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateGrupoInvestigacionCommand>
{
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;
    private readonly IHistorialCoordinadorRepository _historialCoordinadorRepository;
    private readonly IUser _user;

    public UpdateGrupoInvestigacionCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IGrupoInvestigacionRepository grupoinvestigacionRepository,
        IHistorialCoordinadorRepository historialCoordinadorRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
        _historialCoordinadorRepository = historialCoordinadorRepository;
        _user = user;
    }

    public async Task Handle(UpdateGrupoInvestigacionCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var grupoinvestigacion = await _grupoinvestigacionRepository.GetByIdAsync(request.AggregateId);
        if (grupoinvestigacion is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no GrupoInvestigacion with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        if (request.CoordinadorUserId!=null)
        {
            var historialCoordinador = await _historialCoordinadorRepository.GetLatestActiveCoordinadorByGrupoInvestigacionIdAsync(grupoinvestigacion.Id);
            if (historialCoordinador==null || request.CoordinadorUserId!=historialCoordinador.UserId)
            {
                await Bus.SendCommandAsync(new CreateHistorialCoordinadorCommand(
                    grupoinvestigacion.Id,
                    (Guid)request.CoordinadorUserId,
                    request.AggregateId,
                    DateTime.UtcNow));
            }
        }

        grupoinvestigacion.Nombre = request.Nombre;

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new GrupoInvestigacionUpdatedEvent(
                grupoinvestigacion.Id,
                grupoinvestigacion.Nombre));
        }
    }
}
