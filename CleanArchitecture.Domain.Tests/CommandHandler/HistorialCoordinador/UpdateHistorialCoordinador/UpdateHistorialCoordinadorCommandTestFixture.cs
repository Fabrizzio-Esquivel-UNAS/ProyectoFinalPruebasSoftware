using System;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.UpdateHistorialCoordinador;

public sealed class UpdateHistorialCoordinadorCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateHistorialCoordinadorCommandHandler CommandHandler { get; }

    private IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public UpdateHistorialCoordinadorCommandTestFixture()
    {
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();

        CommandHandler = new UpdateHistorialCoordinadorCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            HistorialCoordinadorRepository);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingHistorialCoordinador(Guid id, Guid userId, Guid grupoInvestigacionId)
    {
        HistorialCoordinadorRepository
            .GetByIdAsync(Arg.Is<Guid>(x => x == id))
            .Returns(new Entities.HistorialCoordinador(
                id,
                userId,
                grupoInvestigacionId,
                DateTime.UtcNow,
                null)
            );
    }
}