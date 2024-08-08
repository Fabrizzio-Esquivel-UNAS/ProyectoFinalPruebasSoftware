using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Citas;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Citas.GetCitaById;

public sealed class GetCitaByIdQueryHandler :
    IRequestHandler<GetCitaByIdQuery, CitaViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICitaRepository _citaRepository;

    public GetCitaByIdQueryHandler(ICitaRepository citaRepository, IMediatorHandler bus)
    {
        _citaRepository = citaRepository;
        _bus = bus;
    }

    public async Task<CitaViewModel?> Handle(GetCitaByIdQuery request, CancellationToken cancellationToken)
    {
        var cita = await _citaRepository.GetByIdAsync(request.CitaId);

        if (cita is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCitaByIdQuery),
                    $"Cita with id {request.CitaId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CitaViewModel.FromCita(cita);
    }
}