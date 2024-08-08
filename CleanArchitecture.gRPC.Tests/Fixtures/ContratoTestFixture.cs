using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class ContratoTestFixture
{
    public ContratosApiImplementation ContratosApiImplementation { get; }
    private IContratoRepository ContratoRepository { get; }

    public IEnumerable<Contrato> ExistingContratos { get; }

    public ContratoTestFixture()
    {
        ContratoRepository = Substitute.For<IContratoRepository>();

        ExistingContratos = new List<Contrato>
        {
            new(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow), null),
            new(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10)), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1))),
        };

        ContratoRepository.GetAllNoTracking().Returns(ExistingContratos.BuildMock());

        ContratosApiImplementation = new ContratosApiImplementation(ContratoRepository);
    }
}
