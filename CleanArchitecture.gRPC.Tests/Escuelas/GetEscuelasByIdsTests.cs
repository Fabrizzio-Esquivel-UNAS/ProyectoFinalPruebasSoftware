using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Tests.Fixtures;
using CleanArchitecture.Proto.Escuelas;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.gRPC.Tests.Escuelas;

public sealed class GetEscuelasByIdsTests : IClassFixture<EscuelaTestFixture>
{
    private readonly EscuelaTestFixture _fixture;

    public GetEscuelasByIdsTests(EscuelaTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.EscuelasApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.Escuelas.Should().HaveCount(0);
    }

    [Fact]
    public async Task Should_Get_Requested_Escuelas()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingEscuelas
            .Take(2)
            .Select(escuela => escuela.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.EscuelasApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.Escuelas.Should().HaveCount(2);

        foreach (var escuela in result.Escuelas)
        {
            var escuelaId = Guid.Parse(escuela.Id);

            escuelaId.Should().NotBe(nonExistingId);

            var mockEscuela = _fixture.ExistingEscuelas.First(t => t.Id == escuelaId);

            mockEscuela.Should().NotBeNull();

            escuela.Nombre.Should().Be(mockEscuela.Nombre);
            //escuela.FacultadId.Should().Be(mockEscuela.FacultadId.ToString());
        }
    }

    private static GetEscuelasByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetEscuelasByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}
