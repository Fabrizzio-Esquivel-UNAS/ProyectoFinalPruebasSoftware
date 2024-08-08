using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Escuelas.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Escuelas;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Escuelas;

public sealed class GetAllEscuelasQueryHandlerTests
{
    private readonly GetAllEscuelasTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Escuela()
    {
        var escuela = _fixture.SetupEscuela();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllEscuelasQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        escuela.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Escuela()
    {
        _fixture.SetupEscuela(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllEscuelasQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}
