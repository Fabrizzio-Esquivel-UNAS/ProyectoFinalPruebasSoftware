using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Facultad;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Facultades.DeleteFacultad;

public sealed class DeleteFacultadesCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteFacultadesCommand>
{
    private readonly IFacultadRepository _facultadRepository;
    private readonly IUser _user;
    private readonly IEscuelaRepository _escuelaRepository;

    public DeleteFacultadesCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IFacultadRepository facultadRepository,
        IEscuelaRepository escuelaRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _facultadRepository = facultadRepository;
        _escuelaRepository = escuelaRepository;
        _user = user;
    }

    public async Task Handle(DeleteFacultadesCommand request, CancellationToken cancellationToken)
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

        var facultadEscuelas = _escuelaRepository
            .GetAll()
            .Where(x => x.FacultadId == request.AggregateId);
        
        _escuelaRepository.RemoveRange(facultadEscuelas);

        _facultadRepository.Remove(facultad);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new FacultadDeletedEvent(facultad.Id));
        }
    }
}