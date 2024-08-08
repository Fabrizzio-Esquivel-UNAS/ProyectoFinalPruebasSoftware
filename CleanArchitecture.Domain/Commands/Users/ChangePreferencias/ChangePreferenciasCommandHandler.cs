using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.User;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Users.ChangePreferencias;

public sealed class ChangePreferenciasCommandHandler : CommandHandlerBase,
    IRequestHandler<ChangePreferenciasCommand>
{
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public ChangePreferenciasCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(ChangePreferenciasCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(_user.GetUserId());

        if (user is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Id {_user.GetUserId()}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        user.Preferencias = request.NewPreferencias;

        _userRepository.Update(user);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new PreferenciasChangedEvent(user.Id));
        }
    }
}