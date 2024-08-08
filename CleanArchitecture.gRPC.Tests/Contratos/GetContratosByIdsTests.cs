using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Tests.Fixtures;
using CleanArchitecture.Proto.Contratos;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.gRPC.Tests.Contratos;

public sealed class GetContratosByIdsTests : IClassFixture<ContratoTestFixture>
{
    private readonly ContratoTestFixture _fixture;

    public GetContratosByIdsTests(ContratoTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.ContratosApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.Contratos.Should().HaveCount(0);
    }

    [Fact]
    public async Task Should_Get_Requested_Contratos()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingContratos
            .Take(2)
            .Select(contrato => contrato.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.ContratosApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.Contratos.Should().HaveCount(2);

        foreach (var contrato in result.Contratos)
        {
            var contratoId = Guid.Parse(contrato.Id);

            contratoId.Should().NotBe(nonExistingId);

            var mockContrato = _fixture.ExistingContratos.First(t => t.Id == contratoId);

            mockContrato.Should().NotBeNull();

           // contrato.FechaInicio.Should().Be(mockContrato.FechaInicio.ToString("yyyy-MM-dd"));
            //contrato.FechaFinal.Should().Be(mockContrato.FechaFinal?.ToString("yyyy-MM-dd"));
        }
    }

    private static GetContratosByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetContratosByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}
