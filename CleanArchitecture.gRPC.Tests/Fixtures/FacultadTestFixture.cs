using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class FacultadTestFixture
{
    public FacultadesApiImplementation FacultadesApiImplementation { get; }
    private IFacultadRepository FacultadRepository { get; }

    public IEnumerable<Facultad> ExistingFacultades { get; }

    public FacultadTestFixture()
    {
        FacultadRepository = Substitute.For<IFacultadRepository>();

        ExistingFacultades = new List<Facultad>
        {
            new(Guid.NewGuid(), "Facultad 1"),
            new(Guid.NewGuid(), "Facultad 2"),
        };

        FacultadRepository.GetAllNoTracking().Returns(ExistingFacultades.BuildMock());

        FacultadesApiImplementation = new FacultadesApiImplementation(FacultadRepository);
    }
}
