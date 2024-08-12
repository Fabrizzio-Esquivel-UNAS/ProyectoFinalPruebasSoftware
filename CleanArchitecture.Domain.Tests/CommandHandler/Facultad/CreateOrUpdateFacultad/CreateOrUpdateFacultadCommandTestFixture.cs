using System;
using CleanArchitecture.Domain.Commands.Facultades.CreateFacultad;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Facultad.CreateFacultad;

public sealed class CreateOrUpdateFacultadCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateFacultadesCommandHandler CommandHandler { get; }
    public IFacultadRepository FacultadRepository { get; }

    public CreateOrUpdateFacultadCommandTestFixture()
    {
        FacultadRepository = Substitute.For<IFacultadRepository>();

        CommandHandler = new CreateFacultadesCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            FacultadRepository,
            User);
    }

    public Entities.Facultad SetupFacultad()
    {
        var facultad = new Entities.Facultad(
            Guid.NewGuid(),
            "Nombre de la Facultad");

        FacultadRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == facultad.Id))
            .Returns(facultad);

        return facultad;
    }

    public void SetupExistingFacultad(Guid id)
    {
        FacultadRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}
