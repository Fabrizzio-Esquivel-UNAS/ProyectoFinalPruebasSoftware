using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.LineaInvestigacion;
using MediatR;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.CreateLineaInvestigacion;

public sealed class CreateLineaInvestigacionCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateLineaInvestigacionCommand>
{
    private readonly ILineaInvestigacionRepository _lineainvestigacionRepository;
    private readonly IUser _user;

    public CreateLineaInvestigacionCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ILineaInvestigacionRepository lineainvestigacionRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _lineainvestigacionRepository = lineainvestigacionRepository;
        _user = user;
    }

    public async Task Handle(CreateLineaInvestigacionCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (await _lineainvestigacionRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a lineainvestigacion with Id {request.AggregateId}",
                    DomainErrorCodes.LineaInvestigacion.AlreadyExists));

            return;
        }

        var lineainvestigacion = new LineaInvestigacion(
            request.AggregateId,
            request.FacultadId,
            request.GrupoInvestigacionId,
            request.Nombre);

        _lineainvestigacionRepository.Add(lineainvestigacion);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new LineaInvestigacionCreatedEvent(
                lineainvestigacion.Id,
                lineainvestigacion.Nombre));
        }
    }
}