using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.LineaInvestigacion;
using MediatR;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.UpdateLineaInvestigacion;

public sealed class UpdateLineaInvestigacionCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateLineaInvestigacionCommand>
{
    private readonly ILineaInvestigacionRepository _lineainvestigacionRepository;
    private readonly IUser _user;

    public UpdateLineaInvestigacionCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ILineaInvestigacionRepository lineainvestigacionRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _lineainvestigacionRepository = lineainvestigacionRepository;
        _user = user;
    }

    public async Task Handle(UpdateLineaInvestigacionCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var lineainvestigacion = await _lineainvestigacionRepository.GetByIdAsync(request.AggregateId);

        if (lineainvestigacion is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no lineainvestigacion with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        lineainvestigacion.SetNombre(request.Nombre);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new LineaInvestigacionUpdatedEvent(
                lineainvestigacion.Id,
                lineainvestigacion.Nombre));
        }
    }
}