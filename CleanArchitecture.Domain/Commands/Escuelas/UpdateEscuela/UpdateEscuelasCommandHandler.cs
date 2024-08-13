using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Escuela;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Escuelas.UpdateEscuela;

public sealed class UpdateEscuelasCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateEscuelasCommand>
{
    private readonly IEscuelaRepository _escuelaRepository;
    private readonly IUser _user;

    public UpdateEscuelasCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEscuelaRepository escuelaRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _escuelaRepository = escuelaRepository;
        _user = user;
    }

    public async Task Handle(UpdateEscuelasCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var escuela = await _escuelaRepository.GetByIdAsync(request.AggregateId);

        if (escuela is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no escuela with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        escuela.SetName(request.Nombre);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EscuelaUpdatedEvent(
                escuela.Id,
                escuela.Nombre));
        }
    }
}