using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Tests.Fixtures;
using CleanArchitecture.Proto.Facultades;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.gRPC.Tests.Facultades;

public sealed class GetFacultadesByIdsTests : IClassFixture<FacultadTestFixture>
{
    private readonly FacultadTestFixture _fixture;

    public GetFacultadesByIdsTests(FacultadTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.FacultadesApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.Facultades.Should().HaveCount(0);
    }

    [Fact]
    public async Task Should_Get_Requested_Facultades()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingFacultades
            .Take(2)
            .Select(facultad => facultad.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.FacultadesApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.Facultades.Should().HaveCount(2);

        foreach (var facultad in result.Facultades)
        {
            var facultadId = Guid.Parse(facultad.Id);

            facultadId.Should().NotBe(nonExistingId);

            var mockFacultad = _fixture.ExistingFacultades.First(t => t.Id == facultadId);

            mockFacultad.Should().NotBeNull();

            facultad.Nombre.Should().Be(mockFacultad.Nombre);
        }
    }

    private static GetFacultadesByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetFacultadesByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}

