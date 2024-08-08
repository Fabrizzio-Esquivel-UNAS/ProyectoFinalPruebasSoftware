using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Cita;
using Google.Apis.Calendar.v3.Data;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Citas.DeleteCita;

public sealed class DeleteCitaCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteCitaCommand>
{
    private readonly ICitaRepository _citaRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteCitaCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICitaRepository citaRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _citaRepository = citaRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteCitaCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete cita {request.AggregateId}",
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

        _citaRepository.Remove(cita);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CitaDeletedEvent(cita.Id, cita.EventoId, cita.AsesoradoUserId, cita.AsesoradoUserId));
        }
    }
}