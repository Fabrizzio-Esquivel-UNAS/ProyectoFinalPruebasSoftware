using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Citas;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class CitaControllerTests : IClassFixture<CitaTestFixture>
{
    private readonly CitaTestFixture _fixture;

    public CitaControllerTests(CitaTestFixture fixture)
    {
        _fixture = fixture;
    }
 
    [Fact]
    [Priority(0)]
    public async Task Should_Get_Cita_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/Cita/{_fixture.CreatedCitaId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<CitaViewModel>();

        message?.Data.Should().NotBeNull();

        message!.Data!.Id.Should().Be(_fixture.CreatedCitaId);
    }

    [Fact]
    [Priority(5)]
    public async Task Should_Get_All_Citas()
    {
        var response = await _fixture.ServerClient.GetAsync("api/v1/Cita");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<PagedResult<CitaViewModel>>();

        message?.Data!.Items.Should().NotBeEmpty();
        message!.Data!.Items.Should().HaveCount(1);
        message.Data!.Items
            .FirstOrDefault(x => x.Id == _fixture.CreatedCitaId)
            .Should().NotBeNull();

        message.Data.Items
            .FirstOrDefault(x => x.Id == _fixture.CreatedCitaId);
    }

    [Fact]
    [Priority(20)]
    public async Task Should_Delete_Cita()
    {
        var response = await _fixture.ServerClient.DeleteAsync($"/api/v1/Cita/{_fixture.CreatedCitaId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Check if cita is deleted
        var citaResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Cita/{_fixture.CreatedCitaId}");

        citaResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}