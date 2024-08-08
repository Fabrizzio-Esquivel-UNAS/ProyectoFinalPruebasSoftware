using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Contratos;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Contratos.GetContratoById;

public sealed class GetContratoByIdQueryHandler :
    IRequestHandler<GetContratoByIdQuery, ContratoViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IContratoRepository _contratoRepository;

    public GetContratoByIdQueryHandler(IContratoRepository contratoRepository, IMediatorHandler bus)
    {
        _contratoRepository = contratoRepository;
        _bus = bus;
    }

    public async Task<ContratoViewModel?> Handle(GetContratoByIdQuery request, CancellationToken cancellationToken)
    {
        var contrato = await _contratoRepository.GetByIdAsync(request.ContratoId);

        if (contrato is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetContratoByIdQuery),
                    $"Contrato with id {request.ContratoId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return ContratoViewModel.FromContrato(contrato);
    }
}