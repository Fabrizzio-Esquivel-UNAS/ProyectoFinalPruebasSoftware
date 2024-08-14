using System;
using System.Linq;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.CreateHistorialCoordinador;

public sealed class CreateHistorialCoordinadorCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateHistorialCoordinadorCommandHandler CommandHandler { get; }
    public IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public CreateHistorialCoordinadorCommandTestFixture()
    {
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();

        CommandHandler = new CreateHistorialCoordinadorCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            HistorialCoordinadorRepository);
    }

    public Entities.HistorialCoordinador SetupHistorialCoordinador()
    {
        var historialCoordinador = new Entities.HistorialCoordinador(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            null);

        HistorialCoordinadorRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == historialCoordinador.Id))
            .Returns(historialCoordinador);

        return historialCoordinador;
    }

    public Entities.HistorialCoordinador SetupExistingCoordinadorForDifferentGrupo()
    {
        var historialCoordinador = new Entities.HistorialCoordinador(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            null);

        HistorialCoordinadorRepository
            .GetLatestActiveCoordinadorByUserIdAsync(Arg.Is<Guid>(y => y == historialCoordinador.UserId))
            .Returns(historialCoordinador);

        return historialCoordinador;
    }

    public void SetupExistingHistorialCoordinador(Guid id)
    {
        HistorialCoordinadorRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}
