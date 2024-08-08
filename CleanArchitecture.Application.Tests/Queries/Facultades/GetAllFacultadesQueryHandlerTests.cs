using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Facultades.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Facultades;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Facultades;

public sealed class GetAllFacultadesQueryHandlerTests
{
    private readonly GetAllFacultadesTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Facultad()
    {
        var facultad = _fixture.SetupFacultad();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllFacultadesQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        facultad.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Facultad()
    {
        _fixture.SetupFacultad(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllFacultadesQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}
