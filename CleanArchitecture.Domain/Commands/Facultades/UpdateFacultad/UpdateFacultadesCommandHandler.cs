using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Facultad;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Facultades.UpdateFacultad;

public sealed class UpdateFacultadesCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateFacultadesCommand>
{
    private readonly IFacultadRepository _facultadRepository;
    private readonly IUser _user;

    public UpdateFacultadesCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IFacultadRepository facultadRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _facultadRepository = facultadRepository;
        _user = user;
    }

    public async Task Handle(UpdateFacultadesCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var facultad = await _facultadRepository.GetByIdAsync(request.AggregateId);

        if (facultad is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no facultad with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        facultad.SetName(request.Nombre);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new FacultadUpdatedEvent(
                facultad.Id,
                facultad.Nombre));
        }
    }
}