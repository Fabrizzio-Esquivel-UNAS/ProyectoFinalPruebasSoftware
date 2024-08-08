using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class HistorialCoordinadorTestFixture
{
    public HistorialCoordinadoresApiImplementation HistorialCoordinadoresApiImplementation { get; }
    private IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public IEnumerable<HistorialCoordinador> ExistingHistorialCoordinadores { get; }

    public HistorialCoordinadorTestFixture()
    {
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();

        ExistingHistorialCoordinadores = new List<HistorialCoordinador>
        {
            new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, null),
            new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddDays(-1)),
        };

        HistorialCoordinadorRepository.GetAllNoTracking().Returns(ExistingHistorialCoordinadores.BuildMock());

        HistorialCoordinadoresApiImplementation = new HistorialCoordinadoresApiImplementation(HistorialCoordinadorRepository);
    }
}
