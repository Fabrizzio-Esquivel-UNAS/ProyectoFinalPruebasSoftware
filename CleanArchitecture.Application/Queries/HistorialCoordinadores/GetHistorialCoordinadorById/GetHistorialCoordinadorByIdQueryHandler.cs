using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.HistorialCoordinadores.GetHistorialCoordinadorById;

public sealed class GetHistorialCoordinadorByIdQueryHandler :
    IRequestHandler<GetHistorialCoordinadorByIdQuery, HistorialCoordinadorViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IHistorialCoordinadorRepository _historialcoordinadorRepository;

    public GetHistorialCoordinadorByIdQueryHandler(IHistorialCoordinadorRepository historialcoordinadorRepository, IMediatorHandler bus)
    {
        _historialcoordinadorRepository = historialcoordinadorRepository;
        _bus = bus;
    }

    public async Task<HistorialCoordinadorViewModel?> Handle(GetHistorialCoordinadorByIdQuery request, CancellationToken cancellationToken)
    {
        var historialcoordinador = await _historialcoordinadorRepository.GetByIdAsync(request.HistorialCoordinadorId);

        if (historialcoordinador is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetHistorialCoordinadorByIdQuery),
                    $"HistorialCoordinador with id {request.HistorialCoordinadorId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return HistorialCoordinadorViewModel.FromHistorialCoordinador(historialcoordinador);
    }
}