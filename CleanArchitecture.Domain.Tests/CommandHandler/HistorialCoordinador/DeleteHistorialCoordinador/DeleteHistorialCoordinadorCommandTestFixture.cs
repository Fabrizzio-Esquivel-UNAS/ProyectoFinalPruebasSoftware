using System;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.DeleteHistorialCoordinador;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.DeleteHistorialCoordinador;

public sealed class DeleteHistorialCoordinadorCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteHistorialCoordinadorCommandHandler CommandHandler { get; }

    private IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public DeleteHistorialCoordinadorCommandTestFixture()
    {
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();

        CommandHandler = new DeleteHistorialCoordinadorCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            HistorialCoordinadorRepository);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
}