using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Citas.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Citas;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Citas;

public sealed class GetAllCitasQueryHandlerTests
{
    private readonly GetAllCitasTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Cita()
    {
        var cita = _fixture.SetupCita();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllCitasQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        cita.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Cita()
    {
        _fixture.SetupCita(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllCitasQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}