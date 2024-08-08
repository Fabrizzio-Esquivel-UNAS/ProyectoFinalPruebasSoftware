using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class CitaTestFixture
{
    public CitasApiImplementation CitasApiImplementation { get; }
    private ICitaRepository CitaRepository { get; }

    public IEnumerable<Cita> ExistingCitas { get; }

    public CitaTestFixture()
    {
        CitaRepository = Substitute.For<ICitaRepository>();

        ExistingCitas = new List<Cita>
        {
            new(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid(), Guid.NewGuid(), default, default, default, CitaEstado.Inasistido),
            new(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid(), Guid.NewGuid(), default, default, default, CitaEstado.Completado),
            new(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid(), Guid.NewGuid(), default, default, default, CitaEstado.Justificado),
        };

        CitaRepository.GetAllNoTracking().Returns(ExistingCitas.BuildMock());

        CitasApiImplementation = new CitasApiImplementation(CitaRepository);
    }
}