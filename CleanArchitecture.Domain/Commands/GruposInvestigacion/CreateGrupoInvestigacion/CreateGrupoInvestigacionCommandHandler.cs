using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.GrupoInvestigacion;
using CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;

namespace CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;
public sealed class CreateGrupoInvestigacionCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateGrupoInvestigacionCommand>
{
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;
    private readonly IUser _user;

    public CreateGrupoInvestigacionCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IGrupoInvestigacionRepository grupoinvestigacionRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
        _user = user;
    }

    public async Task Handle(CreateGrupoInvestigacionCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        /*if (await _grupoinvestigacionRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a GrupoInvestigacion with Id {request.AggregateId}",
                    DomainErrorCodes.GrupoInvestigacion.AlreadyExists));

            return;
        }*/

        if (await _grupoinvestigacionRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a GrupoInvestigacion with Id {request.AggregateId}",
                    DomainErrorCodes.GrupoInvestigacion.AlreadyExists));

            return;
        }

        var grupoinvestigacion = new GrupoInvestigacion(
            request.AggregateId,
            request.Nombre);

        _grupoinvestigacionRepository.Add(grupoinvestigacion);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new GrupoInvestigacionCreatedEvent(
                grupoinvestigacion.Id,
                grupoinvestigacion.Nombre));

            if (request.CoordinadorUserId != null)
                await Bus.SendCommandAsync(new CreateHistorialCoordinadorCommand(
                    Guid.NewGuid(),
                    (Guid)request.CoordinadorUserId,
                    request.AggregateId,
                    DateTime.UtcNow));
        }
    }
}
