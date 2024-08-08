using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Users.GetUserById;

public sealed class GetUserByIdQueryHandler :
    IRequestHandler<GetUserByIdQuery, UserViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IUserRepository _userRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IHistorialCoordinadorRepository _historialCoordinadorRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IContratoRepository contratoRepository, ISolicitudRepository solicitudRepository, IMediatorHandler bus, IHistorialCoordinadorRepository historialCoordinadorRepository)
    {
        _userRepository = userRepository;
        _contratoRepository = contratoRepository;
        _solicitudRepository = solicitudRepository;
        _historialCoordinadorRepository = historialCoordinadorRepository;
        _bus = bus;
    }

    public async Task<UserViewModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetUserByIdQuery),
                    $"User with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        Guid? asesorId = null;
        var solicitud = await _solicitudRepository.GetLatestAceptadoSolicitudByAsesoradoUserIdAsync(user.Id);
        if (solicitud != null)
        {
            var contrato = await _contratoRepository.GetBySolicitudIdAsync(solicitud.Id);
            if (contrato != null && contrato.FechaFinal==null)
            {
                asesorId = solicitud.AsesorUserId;
            }
        }

        var userViewModel = UserViewModel.FromUser(user, asesorId);
        if (user.Role == Domain.Enums.UserRole.Coordinador)
        {
            var historialCoordinador = await _historialCoordinadorRepository.GetLatestActiveCoordinadorByUserIdAsync(user.Id);
            if (historialCoordinador != null)
            {
                userViewModel.GrupoInvestigacionId = historialCoordinador.GrupoInvestigacionId;
            }
        }
        return userViewModel;
    }
}