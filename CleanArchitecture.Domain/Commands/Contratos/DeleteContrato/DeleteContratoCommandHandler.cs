using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Contrato;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Contratos.DeleteContrato;

public sealed class DeleteContratoCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteContratoCommand>
{
    private readonly IContratoRepository _contratoRepository;

    public DeleteContratoCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IContratoRepository contratoRepository) : base(bus, unitOfWork, notifications)
    {
        _contratoRepository = contratoRepository;
    }

    public async Task Handle(DeleteContratoCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var contrato = await _contratoRepository.GetByIdAsync(request.AggregateId);

        if (contrato is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no contrato with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        _contratoRepository.Remove(contrato);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ContratoDeletedEvent(contrato.Id));
        }
    }
}