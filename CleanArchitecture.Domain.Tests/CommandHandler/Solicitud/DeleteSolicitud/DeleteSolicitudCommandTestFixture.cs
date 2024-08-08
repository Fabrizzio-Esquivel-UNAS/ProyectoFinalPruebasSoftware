using System;
using CleanArchitecture.Domain.Commands.Solicitudes.DeleteSolicitud;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.DeleteSolicitud;

public sealed class DeleteSolicitudCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteSolicitudCommandHandler CommandHandler { get; }

    private ISolicitudRepository SolicitudRepository { get; }
    private IUserRepository UserRepository { get; }

    public DeleteSolicitudCommandTestFixture()
    {
        SolicitudRepository = Substitute.For<ISolicitudRepository>();
        UserRepository = Substitute.For<IUserRepository>();

        CommandHandler = new DeleteSolicitudCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            SolicitudRepository,
            User);
    }

    public Entities.Solicitud SetupSolicitud()
    {
        var solicitud = new Entities.Solicitud(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            1,
            "Test");

        SolicitudRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == solicitud.Id))
            .Returns(solicitud);

        return solicitud;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
}