using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class GrupoInvestigacionTestFixture
{
    public GruposInvestigacionApiImplementation GruposInvestigacionApiImplementation { get; }
    private IGrupoInvestigacionRepository GrupoInvestigacionRepository { get; }

    public IEnumerable<GrupoInvestigacion> ExistingGruposInvestigacion { get; }

    public GrupoInvestigacionTestFixture()
    {
        GrupoInvestigacionRepository = Substitute.For<IGrupoInvestigacionRepository>();

        ExistingGruposInvestigacion = new List<GrupoInvestigacion>
        {
            new(Guid.NewGuid(), "Grupo de Investigación 1"),
            new(Guid.NewGuid(), "Grupo de Investigación 2"),
        };

        GrupoInvestigacionRepository.GetAllNoTracking().Returns(ExistingGruposInvestigacion.BuildMock());

        GruposInvestigacionApiImplementation = new GruposInvestigacionApiImplementation(GrupoInvestigacionRepository);
    }
}

