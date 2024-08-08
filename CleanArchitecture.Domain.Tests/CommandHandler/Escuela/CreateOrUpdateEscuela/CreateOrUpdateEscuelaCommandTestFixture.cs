using System;
using CleanArchitecture.Domain.Commands.Escuelas.CreateEscuela;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Escuela.CreateEscuela;

public sealed class CreateOrUpdateEscuelaCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateEscuelaCommandHandler CommandHandler { get; }
    public IEscuelaRepository EscuelaRepository { get; }

    public CreateOrUpdateEscuelaCommandTestFixture()
    {
        EscuelaRepository = Substitute.For<IEscuelaRepository>();

        CommandHandler = new CreateEscuelaCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            EscuelaRepository,
            User);
    }

    public Entities.Escuela SetupEscuela()
    {
        var escuela = new Entities.Escuela(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Nombre de la Escuela");

        EscuelaRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == escuela.Id))
            .Returns(escuela);

        return escuela;
    }

    public void SetupExistingEscuela(Guid id)
    {
        EscuelaRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}

