using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Tests.Fixtures;
using CleanArchitecture.Proto.Solicitudes;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.gRPC.Tests.Solicitudes;

public sealed class GetSolicitudesByIdsTests : IClassFixture<SolicitudTestFixture>
{
    private readonly SolicitudTestFixture _fixture;

    public GetSolicitudesByIdsTests(SolicitudTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.SolicitudesApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.Solicitudes.Should().HaveCount(0);
    }

    [Fact]
    public async Task? Should_Get_Requested_Solicitudes()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingSolicitudes
            .Take(2)
            .Select(solicitud => solicitud.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.SolicitudesApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.Solicitudes.Should().HaveCount(2);

        foreach (var solicitud in result.Solicitudes)
        {
            var solicitudId = Guid.Parse(solicitud.Id);

            solicitudId.Should().NotBe(nonExistingId);

            var mockSolicitud = _fixture.ExistingSolicitudes.First(t => t.Id == solicitudId);

            mockSolicitud.Should().NotBeNull();

        }
    }

    private static GetSolicitudesByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetSolicitudesByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}