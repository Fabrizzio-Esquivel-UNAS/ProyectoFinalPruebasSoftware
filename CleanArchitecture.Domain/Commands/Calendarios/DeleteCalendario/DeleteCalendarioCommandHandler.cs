using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Calendario;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Calendarios.DeleteCalendario;

public sealed class DeleteCalendarioCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteCalendarioCommand>
{
    private readonly ICalendarioRepository _calendarioRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteCalendarioCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICalendarioRepository calendarioRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _calendarioRepository = calendarioRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteCalendarioCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Asesor)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete calendario {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var calendario = await _calendarioRepository.GetByIdAsync(request.AggregateId);

        if (calendario is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no calendario with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        _calendarioRepository.Remove(calendario);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CalendarioDeletedEvent(calendario.Id));
        }
    }
}