using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Escuela;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Escuelas.DeleteEscuela;

public sealed class DeleteEscuelasCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteEscuelasCommand>
{
    private readonly IEscuelaRepository _escuelaRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteEscuelasCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEscuelaRepository escuelaRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _escuelaRepository = escuelaRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteEscuelasCommand request, CancellationToken cancellationToken)
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

        _escuelaRepository.Remove(escuela);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EscuelaDeletedEvent(escuela.Id));
        }
    }
}