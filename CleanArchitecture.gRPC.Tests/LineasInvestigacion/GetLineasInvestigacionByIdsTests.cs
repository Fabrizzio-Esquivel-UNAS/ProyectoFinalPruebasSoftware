using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Tests.Fixtures;
using CleanArchitecture.Proto.LineasInvestigacion;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.gRPC.Tests.LineasInvestigacion;

public sealed class GetLineasInvestigacionByIdsTests : IClassFixture<LineaInvestigacionTestFixture>
{
    private readonly LineaInvestigacionTestFixture _fixture;

    public GetLineasInvestigacionByIdsTests(LineaInvestigacionTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.LineasInvestigacionApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.LineasInvestigacion.Should().HaveCount(0);
    }

    [Fact]
    public async Task Should_Get_Requested_LineasInvestigacion()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingLineasInvestigacion
            .Take(2)
            .Select(lineainvestigacion => lineainvestigacion.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.LineasInvestigacionApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.LineasInvestigacion.Should().HaveCount(2);

        foreach (var lineainvestigacion in result.LineasInvestigacion)
        {
            var lineainvestigacionId = Guid.Parse(lineainvestigacion.Id);

            lineainvestigacionId.Should().NotBe(nonExistingId);

            var mockLineaInvestigacion = _fixture.ExistingLineasInvestigacion.First(t => t.Id == lineainvestigacionId);

            mockLineaInvestigacion.Should().NotBeNull();

            lineainvestigacion.Nombre.Should().Be(mockLineaInvestigacion.Nombre);
        }
    }

    private static GetLineasInvestigacionByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetLineasInvestigacionByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}
