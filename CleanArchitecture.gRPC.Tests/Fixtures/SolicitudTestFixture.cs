using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class SolicitudTestFixture
{
    public SolicitudesApiImplementation SolicitudesApiImplementation { get; }
    private ISolicitudRepository SolicitudRepository { get; }

    public IEnumerable<Solicitud> ExistingSolicitudes { get; }

    public SolicitudTestFixture()
    {
        SolicitudRepository = Substitute.For<ISolicitudRepository>();

        ExistingSolicitudes = new List<Solicitud>
        {
            new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1, "Solicitud 1"),
            new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1, "Solicitud 2"),
            new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1, "Solicitud 3"),
        };

        SolicitudRepository.GetAllNoTracking().Returns(ExistingSolicitudes.BuildMock());

        SolicitudesApiImplementation = new SolicitudesApiImplementation(SolicitudRepository);
    }
}