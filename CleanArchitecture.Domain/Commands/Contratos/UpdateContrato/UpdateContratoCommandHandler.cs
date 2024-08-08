using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Contrato;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Contratos.UpdateContrato;

public sealed class UpdateContratoCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateContratoCommand>
{
    private readonly IContratoRepository _contratoRepository;

    public UpdateContratoCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IContratoRepository contratoRepository) : base(bus, unitOfWork, notifications)
    {
        _contratoRepository = contratoRepository;
    }

    public async Task Handle(UpdateContratoCommand request, CancellationToken cancellationToken)
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

        contrato.FechaFinal = DateOnly.FromDateTime(DateTime.UtcNow);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ContratoUpdatedEvent(
                contrato.Id,
                contrato.SolicitudId,
                contrato.FechaInicio,
                (DateOnly)contrato.FechaFinal));
        }
    }
}