using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Escuela;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Escuelas.CreateEscuela;

public sealed class CreateEscuelasCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateEscuelasCommand>
{
    private readonly IEscuelaRepository _escuelaRepository;
    private readonly IUser _user;

    public CreateEscuelasCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEscuelaRepository escuelaRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _escuelaRepository = escuelaRepository;
        _user = user;
    }

    public async Task Handle(CreateEscuelasCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (await _escuelaRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a escuela with Id {request.AggregateId}",
                    DomainErrorCodes.Escuela.AlreadyExists));

            return;
        }

        var escuela = new Escuela(
            request.AggregateId,
            request.FacultadId,
            request.Nombre);

        _escuelaRepository.Add(escuela);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EscuelaCreatedEvent(
                escuela.Id,
                escuela.Nombre));
        }
    }
}