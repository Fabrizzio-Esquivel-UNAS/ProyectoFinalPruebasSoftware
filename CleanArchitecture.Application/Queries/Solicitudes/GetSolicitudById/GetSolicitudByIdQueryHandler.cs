using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Solicitudes.GetSolicitudById;

public sealed class GetSolicitudByIdQueryHandler :
    IRequestHandler<GetSolicitudByIdQuery, SolicitudViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ISolicitudRepository _solicitudRepository;

    public GetSolicitudByIdQueryHandler(ISolicitudRepository solicitudRepository, IMediatorHandler bus)
    {
        _solicitudRepository = solicitudRepository;
        _bus = bus;
    }

    public async Task<SolicitudViewModel?> Handle(GetSolicitudByIdQuery request, CancellationToken cancellationToken)
    {
        var solicitud = await _solicitudRepository.GetByIdAsync(request.SolicitudId);

        if (solicitud is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetSolicitudByIdQuery),
                    $"Solicitud with id {request.SolicitudId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return SolicitudViewModel.FromSolicitud(solicitud);
    }
}