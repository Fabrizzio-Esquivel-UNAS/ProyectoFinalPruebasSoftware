using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.LineasInvestigacion.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.LineasInvestigacion;

public sealed class GetAllLineasInvestigacionTestFixture : QueryHandlerBaseFixture
{
    public GetAllLineasInvestigacionQueryHandler QueryHandler { get; }
    private ILineaInvestigacionRepository LineaInvestigacionRepository { get; }

    public GetAllLineasInvestigacionTestFixture()
    {
        LineaInvestigacionRepository = Substitute.For<ILineaInvestigacionRepository>();
        var sortingProvider = new LineaInvestigacionViewModelSortProvider();

        QueryHandler = new GetAllLineasInvestigacionQueryHandler(LineaInvestigacionRepository, sortingProvider);
    }

    public LineaInvestigacion SetupLineaInvestigacion(bool deleted = false)
    {
        var lineaInvestigacion = new LineaInvestigacion(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Nombre de la Línea de Investigación");

        if (deleted)
        {
            lineaInvestigacion.Delete();
        }

        var lineaInvestigacionList = new List<LineaInvestigacion> { lineaInvestigacion }.BuildMock();
        LineaInvestigacionRepository.GetAllNoTracking().Returns(lineaInvestigacionList);

        return lineaInvestigacion;
    }
}
