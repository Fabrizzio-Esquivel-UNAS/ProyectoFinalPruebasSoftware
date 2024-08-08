using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.LineaInvestigacion;
using MediatR;

namespace CleanArchitecture.Domain.Commands.LineasInvestigacion.DeleteLineaInvestigacion;

public sealed class DeleteLineaInvestigacionCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteLineaInvestigacionCommand>
{
    private readonly ILineaInvestigacionRepository _lineainvestigacionRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteLineaInvestigacionCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ILineaInvestigacionRepository lineainvestigacionRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _lineainvestigacionRepository = lineainvestigacionRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteLineaInvestigacionCommand request, CancellationToken cancellationToken)
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

        _lineainvestigacionRepository.Remove(lineainvestigacion);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new LineaInvestigacionDeletedEvent(lineainvestigacion.Id));
        }
    }
}