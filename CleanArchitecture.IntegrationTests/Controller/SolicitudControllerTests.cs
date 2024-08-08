using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class SolicitudControllerTests : IClassFixture<SolicitudTestFixture>
{
    private readonly SolicitudTestFixture _fixture;

    public SolicitudControllerTests(SolicitudTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Priority(0)]
    public async Task Should_Get_Solicitud_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/Solicitud/{_fixture.CreatedSolicitudId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<SolicitudViewModel>();

        message?.Data.Should().NotBeNull();

        message!.Data!.Id.Should().Be(_fixture.CreatedSolicitudId);
        message.Data.Mensaje.Should().Be("Test Solicitud");
    }

    [Fact]
    [Priority(5)]
    public async Task Should_Get_All_Solicitudes()
    {
        var response = await _fixture.ServerClient.GetAsync(
            "api/v1/Solicitud?searchTerm=Test&pageSize=5&page=1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<PagedResult<SolicitudViewModel>>();

        message?.Data!.Items.Should().NotBeEmpty();
        message!.Data!.Items.Should().HaveCount(1);
        message.Data!.Items
            .FirstOrDefault(x => x.Id == _fixture.CreatedSolicitudId)
            .Should().NotBeNull();
    }

    [Fact]
    [Priority(10)]
    public async Task Should_Create_Solicitud()
    {
        var request = new CreateSolicitudViewModel(
            Ids.Seed.AsesoradoUserId,
            Ids.Seed.AsesorUserId,
            1,
            "Test Solicitud 2");

        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/Solicitud", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<Guid>();
        var solicitudId = message?.Data;

        // Check if solicitud exists
        var solicitudResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Solicitud/{solicitudId}");

        solicitudResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var solicitudMessage = await solicitudResponse.Content.ReadAsJsonAsync<SolicitudViewModel>();

        solicitudMessage?.Data.Should().NotBeNull();

        solicitudMessage!.Data!.Id.Should().Be(solicitudId!.Value);
        solicitudMessage.Data.Mensaje.Should().Be(request.Mensaje);
    }

    [Fact]
    [Priority(15)]
    public async Task Should_Update_Solicitud()
    {
        var request = new UpdateSolicitudViewModel(
            _fixture.CreatedSolicitudId,
            SolicitudStatus.Rechazado);

        var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/Solicitud", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<UpdateSolicitudViewModel>();

        message?.Data.Should().NotBeNull();
        message!.Data.Should().BeEquivalentTo(request);

        // Check if solicitud is updated
        var solicitudResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Solicitud/{_fixture.CreatedSolicitudId}");

        solicitudResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var solicitudMessage = await response.Content.ReadAsJsonAsync<SolicitudViewModel>();

        solicitudMessage?.Data.Should().NotBeNull();

        solicitudMessage!.Data!.Id.Should().Be(_fixture.CreatedSolicitudId);
        solicitudMessage.Data.Estado.Should().Be(request.Estado);
    }

    [Fact]
    [Priority(20)]
    public async Task Should_Delete_Solicitud()
    {
        var response = await _fixture.ServerClient.DeleteAsync($"/api/v1/Solicitud/{_fixture.CreatedSolicitudId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Check if solicitud is deleted
        var solicitudResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Solicitud/{_fixture.CreatedSolicitudId}");

        solicitudResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}