using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class EscuelaTestFixture
{
    public EscuelasApiImplementation EscuelasApiImplementation { get; }
    private IEscuelaRepository EscuelaRepository { get; }

    public IEnumerable<Escuela> ExistingEscuelas { get; }

    public EscuelaTestFixture()
    {
        EscuelaRepository = Substitute.For<IEscuelaRepository>();

        ExistingEscuelas = new List<Escuela>
        {
            new(Guid.NewGuid(), Guid.NewGuid(), "Escuela 1"),
            new(Guid.NewGuid(), Guid.NewGuid(), "Escuela 2"),
        };

        EscuelaRepository.GetAllNoTracking().Returns(ExistingEscuelas.BuildMock());

        EscuelasApiImplementation = new EscuelasApiImplementation(EscuelaRepository);
    }
}
