using System;
using CleanArchitecture.Application.Queries.Contratos.GetContratoById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Contratos;

public sealed class GetContratoByIdTestFixture : QueryHandlerBaseFixture
{
    public GetContratoByIdQueryHandler QueryHandler { get; }
    private IContratoRepository ContratoRepository { get; }

    public GetContratoByIdTestFixture()
    {
        ContratoRepository = Substitute.For<IContratoRepository>();

        QueryHandler = new GetContratoByIdQueryHandler(
            ContratoRepository,
            Bus);
    }

    public Contrato SetupContrato(bool deleted = false)
    {
        var contrato = new Contrato(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Now), null);

        if (deleted)
        {
            contrato.Delete();
        }
        else
        {
            ContratoRepository.GetByIdAsync(Arg.Is<Guid>(y => y == contrato.Id)).Returns(contrato);
        }

        return contrato;
    }
}

