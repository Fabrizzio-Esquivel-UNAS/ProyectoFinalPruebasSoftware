using System;
using CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.UpdateSolicitud;

public sealed class UpdateSolicitudCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateSolicitudCommandHandler CommandHandler { get; }

    private ISolicitudRepository SolicitudRepository { get; }

    public UpdateSolicitudCommandTestFixture()
    {
        SolicitudRepository = Substitute.For<ISolicitudRepository>();

        CommandHandler = new UpdateSolicitudCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            SolicitudRepository,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingSolicitud(Guid id)
    {
        SolicitudRepository
            .GetByIdAsync(Arg.Is<Guid>(x => x == id))
            .Returns(new Entities.Solicitud(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                1,
                "Test")
            );
    }
}