using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Contrato;
using MediatR;
using Microsoft.VisualBasic;

namespace CleanArchitecture.Domain.Commands.Contratos.CreateContrato;

public sealed class CreateContratoCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateContratoCommand>
{
    private readonly IContratoRepository _contratoRepository;

    public CreateContratoCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IContratoRepository contratoRepository) : base(bus, unitOfWork, notifications)
    {
        _contratoRepository = contratoRepository;
    }

    public async Task Handle(CreateContratoCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (await _contratoRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a contrato with Id {request.AggregateId}",
                    DomainErrorCodes.Contrato.AlreadyExists));

            return;
        }

        var contrato = new Contrato(
            request.ContratoId,
            request.SolicitudId,
            DateOnly.FromDateTime(DateTime.UtcNow)
            );

        _contratoRepository.Add(contrato);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ContratoCreatedEvent(
                contrato.Id,
                contrato.SolicitudId,
                contrato.FechaInicio));
        }
    }
}