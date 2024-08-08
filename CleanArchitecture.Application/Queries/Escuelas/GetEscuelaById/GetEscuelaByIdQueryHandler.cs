using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Escuelas;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Escuelas.GetEscuelasById;

public sealed class GetEscuelasByIdQueryHandler :
    IRequestHandler<GetEscuelasByIdQuery, EscuelaViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IEscuelaRepository _escuelaRepository;

    public GetEscuelasByIdQueryHandler(IEscuelaRepository escuelaRepository, IMediatorHandler bus)
    {
        _escuelaRepository = escuelaRepository;
        _bus = bus;
    }

    public async Task<EscuelaViewModel?> Handle(GetEscuelasByIdQuery request, CancellationToken cancellationToken)
    {
        var escuela = await _escuelaRepository.GetByIdAsync(request.EscuelasId);

        if (escuela is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetEscuelasByIdQuery),
                    $"Escuela with id {request.EscuelasId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return EscuelaViewModel.FromEscuelas(escuela);
    }
}