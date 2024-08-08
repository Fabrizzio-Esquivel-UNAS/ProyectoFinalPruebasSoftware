using System;
using CleanArchitecture.Domain.Commands.LineasInvestigacion.CreateLineaInvestigacion;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.LineaInvestigacion.CreateLineaInvestigacion;

public sealed class CreateOrUpdateLineaInvestigacionCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateLineaInvestigacionCommandHandler CommandHandler { get; }
    public ILineaInvestigacionRepository LineaInvestigacionRepository { get; }

    public CreateOrUpdateLineaInvestigacionCommandTestFixture()
    {
        LineaInvestigacionRepository = Substitute.For<ILineaInvestigacionRepository>();

        CommandHandler = new CreateLineaInvestigacionCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            LineaInvestigacionRepository,
            User);
    }

    public Entities.LineaInvestigacion SetupLineaInvestigacion()
    {
        var lineaInvestigacion = new Entities.LineaInvestigacion(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Nombre de la Línea de Investigación");

        LineaInvestigacionRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == lineaInvestigacion.Id))
            .Returns(lineaInvestigacion);

        return lineaInvestigacion;
    }

    public void SetupExistingLineaInvestigacion(Guid id)
    {
        LineaInvestigacionRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}

