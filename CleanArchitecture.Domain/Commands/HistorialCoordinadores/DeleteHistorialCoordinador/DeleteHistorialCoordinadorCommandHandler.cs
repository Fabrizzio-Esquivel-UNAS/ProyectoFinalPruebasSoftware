using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.HistorialCoordinador;
using MediatR;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.DeleteHistorialCoordinador;

public sealed class DeleteHistorialCoordinadorCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteHistorialCoordinadorCommand>
{
    private readonly IHistorialCoordinadorRepository _historialcoordinadorRepository;

    public DeleteHistorialCoordinadorCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IHistorialCoordinadorRepository historialcoordinadorRepository) : base(bus, unitOfWork, notifications)
    {
        _historialcoordinadorRepository = historialcoordinadorRepository;
    }

    public async Task Handle(DeleteHistorialCoordinadorCommand request, CancellationToken cancellationToken)
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

        _historialcoordinadorRepository.Remove(historialcoordinador);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new HistorialCoordinadorDeletedEvent(historialcoordinador.Id));
        }
    }
}