using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Solicitud;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Solicitudes.DeleteSolicitud;

public sealed class DeleteSolicitudCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteSolicitudCommand>
{
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IUser _user;
    public DeleteSolicitudCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISolicitudRepository solicitudRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _solicitudRepository = solicitudRepository;
        //_asesoradoRepository = asesoradoRepository;
        //_asesorRepository = asesorRepository;
        _user = user;
    }

    public async Task Handle(DeleteSolicitudCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete solicitud {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var solicitud = await _solicitudRepository.GetByIdAsync(request.AggregateId);

        if (solicitud is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no solicitud with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }
        /*

        var solicitudAsesorados = _asesoradoRepository
            .GetAll()
            .Where(x => x.SolicitudId == request.AggregateId);

        var solicitudAsesores = _asesorRepository
            .GetAll()
            .Where(x => x.SolicitudId == request.AggregateId);

        _asesoradoRepository.RemoveRange(solicitudAsesorados);
        _asesorRepository.RemoveRange(solicitudAsesores);
        */

        _solicitudRepository.Remove(solicitud);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new SolicitudDeletedEvent(solicitud.Id));
        }
    }
}