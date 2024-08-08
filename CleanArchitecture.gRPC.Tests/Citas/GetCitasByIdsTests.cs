using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Tests.Fixtures;
using CleanArchitecture.Proto.Citas;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.gRPC.Tests.Citas;

public sealed class GetCitasByIdsTests : IClassFixture<CitaTestFixture>
{
    private readonly CitaTestFixture _fixture;

    public GetCitasByIdsTests(CitaTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.CitasApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.Citas.Should().HaveCount(0);
    }

    [Fact]
    public async Task? Should_Get_Requested_Citas()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingCitas
            .Take(2)
            .Select(cita => cita.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.CitasApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.Citas.Should().HaveCount(2);

        foreach (var cita in result.Citas)
        {
            var citaId = Guid.Parse(cita.Id);

            citaId.Should().NotBe(nonExistingId);

            var mockCita = _fixture.ExistingCitas.First(t => t.Id == citaId);

            mockCita.Should().NotBeNull();

            cita.EventoId.Should().Be(mockCita.EventoId);
        }
    }

    private static GetCitasByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetCitasByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}