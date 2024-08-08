using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class LineaInvestigacionTestFixture
{
    public LineasInvestigacionApiImplementation LineasInvestigacionApiImplementation { get; }
    private ILineaInvestigacionRepository LineaInvestigacionRepository { get; }

    public IEnumerable<LineaInvestigacion> ExistingLineasInvestigacion { get; }

    public LineaInvestigacionTestFixture()
    {
        LineaInvestigacionRepository = Substitute.For<ILineaInvestigacionRepository>();

        ExistingLineasInvestigacion = new List<LineaInvestigacion>
        {
            new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Nombre de la LineaInvestigacion 1"),
            new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Nombre de la LineaInvestigacion 2"),
            new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Nombre de la LineaInvestigacion 3"),
        };

        LineaInvestigacionRepository.GetAllNoTracking().Returns(ExistingLineasInvestigacion.BuildMock());

        LineasInvestigacionApiImplementation = new LineasInvestigacionApiImplementation(LineaInvestigacionRepository);
    }
}
