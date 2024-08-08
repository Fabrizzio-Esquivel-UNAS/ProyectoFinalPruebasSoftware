using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Events.GrupoInvestigacion;
using CleanArchitecture.Domain.Commands.GruposInvestigacion.DeleteGrupoInvestigacion;

namespace CleanArchitecture.Domain.Commands.GruposInvestigacion.DeleteGrupoInvestigacion;

public sealed class DeleteGrupoInvestigacionCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteGrupoInvestigacionCommand>
{
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteGrupoInvestigacionCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IGrupoInvestigacionRepository grupoinvestigacionRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteGrupoInvestigacionCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var grupoinvestigacion = await _grupoinvestigacionRepository.GetByIdAsync(request.AggregateId);

        if (grupoinvestigacion is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no grupoinvestigacion with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        _grupoinvestigacionRepository.Remove(grupoinvestigacion);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new GrupoInvestigacionDeletedEvent(grupoinvestigacion.Id));
        }
    }
}

