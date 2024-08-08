using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.HistorialCoordinador;
using MediatR;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;

public sealed class UpdateHistorialCoordinadorCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateHistorialCoordinadorCommand>
{
    private readonly IHistorialCoordinadorRepository _historialcoordinadorRepository;

    public UpdateHistorialCoordinadorCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IHistorialCoordinadorRepository historialcoordinadorRepository) : base(bus, unitOfWork, notifications)
    {
        _historialcoordinadorRepository = historialcoordinadorRepository;
    }

    public async Task Handle(UpdateHistorialCoordinadorCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var historialcoordinador = await _historialcoordinadorRepository.GetByIdAsync(request.AggregateId);

        if (historialcoordinador is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no historialcoordinador with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        historialcoordinador.SetFechaFin(request.FechaFin);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new HistorialCoordinadorUpdatedEvent(
                historialcoordinador.Id,
                historialcoordinador.UserId,
                historialcoordinador.GrupoInvestigacionId,
                historialcoordinador.FechaInicio,
                historialcoordinador.FechaFin));
        }
    }
}