using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.Contratos.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Contratos;

public sealed class GetAllContratosTestFixture : QueryHandlerBaseFixture
{
    public GetAllContratosQueryHandler QueryHandler { get; }
    private IContratoRepository ContratoRepository { get; }

    public GetAllContratosTestFixture()
    {
        ContratoRepository = Substitute.For<IContratoRepository>();
        var sortingProvider = new ContratoViewModelSortProvider();

        QueryHandler = new GetAllContratosQueryHandler(ContratoRepository, sortingProvider);
    }

    public Contrato SetupContrato(bool deleted = false)
    {
        var contrato = new Contrato(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Now), null);

        if (deleted)
        {
            contrato.Delete();
        }

        var contratoList = new List<Contrato> { contrato }.BuildMock();
        ContratoRepository.GetAllNoTracking().Returns(contratoList);

        return contrato;
    }
}
