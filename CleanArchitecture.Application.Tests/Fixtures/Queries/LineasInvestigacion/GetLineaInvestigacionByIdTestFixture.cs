using System;
using CleanArchitecture.Application.Queries.LineasInvestigacion.GetLineaInvestigacionById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.LineasInvestigacion;

public sealed class GetLineaInvestigacionByIdTestFixture : QueryHandlerBaseFixture
{
    public GetLineaInvestigacionByIdQueryHandler QueryHandler { get; }
    private ILineaInvestigacionRepository LineaInvestigacionRepository { get; }

    public GetLineaInvestigacionByIdTestFixture()
    {
        LineaInvestigacionRepository = Substitute.For<ILineaInvestigacionRepository>();

        QueryHandler = new GetLineaInvestigacionByIdQueryHandler(
            LineaInvestigacionRepository,
            Bus);
    }

    public LineaInvestigacion SetupLineaInvestigacion(bool deleted = false)
    {
        var lineaInvestigacion = new LineaInvestigacion(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Nombre de la Línea de Investigación");

        if (deleted)
        {
            lineaInvestigacion.Delete();
        }
        else
        {
            LineaInvestigacionRepository.GetByIdAsync(Arg.Is<Guid>(y => y == lineaInvestigacion.Id)).Returns(lineaInvestigacion);
        }

        return lineaInvestigacion;
    }
}
