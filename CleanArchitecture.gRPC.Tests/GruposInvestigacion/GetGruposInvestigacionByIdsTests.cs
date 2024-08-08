using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Tests.Fixtures;
using CleanArchitecture.Proto.GruposInvestigacion;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.gRPC.Tests.GruposInvestigacion;

public sealed class GetGruposInvestigacionByIdsTests : IClassFixture<GrupoInvestigacionTestFixture>
{
    private readonly GrupoInvestigacionTestFixture _fixture;

    public GetGruposInvestigacionByIdsTests(GrupoInvestigacionTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.GruposInvestigacionApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.GruposInvestigacion.Should().HaveCount(0);
    }

    [Fact]
    public async Task Should_Get_Requested_GruposInvestigacion()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingGruposInvestigacion
            .Take(2)
            .Select(grupoInvestigacion => grupoInvestigacion.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.GruposInvestigacionApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.GruposInvestigacion.Should().HaveCount(2);

        foreach (var grupoInvestigacion in result.GruposInvestigacion)
        {
            var grupoInvestigacionId = Guid.Parse(grupoInvestigacion.Id);

            grupoInvestigacionId.Should().NotBe(nonExistingId);

            var mockGrupoInvestigacion = _fixture.ExistingGruposInvestigacion.First(t => t.Id == grupoInvestigacionId);

            mockGrupoInvestigacion.Should().NotBeNull();

            grupoInvestigacion.Nombre.Should().Be(mockGrupoInvestigacion.Nombre);
        }
    }

    private static GetGruposInvestigacionByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetGruposInvestigacionByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}

