using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Tests.Fixtures;
using CleanArchitecture.Proto.HistorialCoordinadores;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.gRPC.Tests.HistorialCoordinadores;

public sealed class GetHistorialCoordinadoresByIdsTests : IClassFixture<HistorialCoordinadorTestFixture>
{
    private readonly HistorialCoordinadorTestFixture _fixture;

    public GetHistorialCoordinadoresByIdsTests(HistorialCoordinadorTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.HistorialCoordinadoresApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.Historialcoordinadores.Should().HaveCount(0);
    }

    [Fact]
    public async Task Should_Get_Requested_HistorialCoordinadores()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingHistorialCoordinadores
            .Take(2)
            .Select(historialCoordinador => historialCoordinador.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.HistorialCoordinadoresApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.Historialcoordinadores.Should().HaveCount(2);

        foreach (var historialCoordinador in result.Historialcoordinadores)
        {
            var historialCoordinadorId = Guid.Parse(historialCoordinador.Id);

            historialCoordinadorId.Should().NotBe(nonExistingId);

            var mockHistorialCoordinador = _fixture.ExistingHistorialCoordinadores.First(t => t.Id == historialCoordinadorId);

            mockHistorialCoordinador.Should().NotBeNull();

            historialCoordinador.UserId.Should().Be(mockHistorialCoordinador.UserId.ToString());
            historialCoordinador.GrupoinvestigacionId.Should().Be(mockHistorialCoordinador.GrupoInvestigacionId.ToString());
           // historialCoordinador.Fechainicio.Should().Be(mockHistorialCoordinador.FechaInicio.ToString("yyyy-MM-dd"));
           // historialCoordinador.Fechafin.Should().Be(mockHistorialCoordinador.FechaFin?.ToString("yyyy-MM-dd"));
        }
    }

    private static GetHistorialCoordinadoresByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetHistorialCoordinadoresByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}

