using System;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.IntegrationTests.Fixtures.gRPC;
using CleanArchitecture.Proto.Solicitudes;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.gRPC;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class GetSolicitudesByIdsTests : IClassFixture<GetSolicitudesByIdsTestFixture>
{
    private readonly GetSolicitudesByIdsTestFixture _fixture;

    public GetSolicitudesByIdsTests(GetSolicitudesByIdsTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Solicitudes_By_Ids()
    {
        var client = new SolicitudesApi.SolicitudesApiClient(_fixture.GrpcChannel);

        var request = new GetSolicitudesByIdsRequest();
        request.Ids.Add(_fixture.CreatedSolicitudId.ToString());

        var response = await client.GetByIdsAsync(request);

        response.Solicitudes.Should().HaveCount(1);

        var tenant = response.Solicitudes.First();
        var createdSolicitud = _fixture.CreateSolicitud();

        new Guid(tenant.Id).Should().Be(createdSolicitud.Id);
    }
}