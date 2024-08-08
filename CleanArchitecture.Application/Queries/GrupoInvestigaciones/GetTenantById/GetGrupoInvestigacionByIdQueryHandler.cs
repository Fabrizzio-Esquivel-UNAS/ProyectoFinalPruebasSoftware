using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.GruposInvestigacion;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.GruposInvestigacion.GetGrupoInvestigacionById;

public sealed class GetGrupoInvestigacionByIdQueryHandler :
    IRequestHandler<GetGrupoInvestigacionByIdQuery, GrupoInvestigacionViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;
    private readonly IHistorialCoordinadorRepository _historialCoordinadorRepository;

    public GetGrupoInvestigacionByIdQueryHandler(
        IGrupoInvestigacionRepository grupoinvestigacionRepository, 
        IHistorialCoordinadorRepository historialCoordinadorRepository, 
        IMediatorHandler bus)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
        _historialCoordinadorRepository = historialCoordinadorRepository;
        _bus = bus;
    }

    public async Task<GrupoInvestigacionViewModel?> Handle(GetGrupoInvestigacionByIdQuery request, CancellationToken cancellationToken)
    {
        var grupoinvestigacion = await _grupoinvestigacionRepository.GetByIdAsync(request.GrupoInvestigacionId);

        if (grupoinvestigacion is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetGrupoInvestigacionByIdQuery),
                    $"GrupoInvestigacion with id {request.GrupoInvestigacionId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        var grupoInvestigacionViewModel = GrupoInvestigacionViewModel.FromGrupoInvestigacion(grupoinvestigacion);
        var historialCoordinador = await _historialCoordinadorRepository.GetLatestActiveCoordinadorByGrupoInvestigacionIdAsync(grupoinvestigacion.Id);
        if (historialCoordinador != null)
        {
            grupoInvestigacionViewModel.CoordinadorUserId = historialCoordinador.UserId;
        }
        return grupoInvestigacionViewModel;
    }
}