using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Facultad;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Facultades.CreateFacultad;

public sealed class CreateFacultadesCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateFacultadesCommand>
{
    private readonly IFacultadRepository _facultadRepository;
    private readonly IUser _user;

    public CreateFacultadesCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IFacultadRepository facultadRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _facultadRepository = facultadRepository;
        _user = user;
    }

    public async Task Handle(CreateFacultadesCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create facultad {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }
        */

        if (await _facultadRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a facultad with Id {request.AggregateId}",
                    DomainErrorCodes.Facultad.AlreadyExists));

            return;
        }

        var facultad = new Facultad(
            request.AggregateId,
            request.Nombre);

        _facultadRepository.Add(facultad);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new FacultadCreatedEvent(
                facultad.Id,
                facultad.Nombre));
        }
    }
}