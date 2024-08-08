using System;
using CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.GrupoInvestigacion.CreateGrupoInvestigacion;

public sealed class CreateOrUpdateGrupoInvestigacionCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateGrupoInvestigacionCommandHandler CommandHandler { get; }
    public IGrupoInvestigacionRepository GrupoInvestigacionRepository { get; }

    public CreateOrUpdateGrupoInvestigacionCommandTestFixture()
    {
        GrupoInvestigacionRepository = Substitute.For<IGrupoInvestigacionRepository>();

        CommandHandler = new CreateGrupoInvestigacionCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            GrupoInvestigacionRepository,
            User);
    }

    public Entities.GrupoInvestigacion SetupGrupoInvestigacion()
    {
        var grupoInvestigacion = new Entities.GrupoInvestigacion(
            Guid.NewGuid(),
            "Nombre del Grupo");

        grupoInvestigacion.HistorialCoordinadores.Add(new Entities.HistorialCoordinador(
            Guid.NewGuid(),
            Guid.NewGuid(),
            grupoInvestigacion.Id,
            DateTime.UtcNow,
            null));

        GrupoInvestigacionRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == grupoInvestigacion.Id))
            .Returns(grupoInvestigacion);

        return grupoInvestigacion;
    }

    public void SetupExistingGrupoInvestigacion(Guid id)
    {
        GrupoInvestigacionRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}
