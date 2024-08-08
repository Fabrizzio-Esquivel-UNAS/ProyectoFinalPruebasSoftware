using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.LineasInvestigacion.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.LineasInvestigacion;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.LineasInvestigacion;

public sealed class GetAllLineasInvestigacionQueryHandlerTests
{
    private readonly GetAllLineasInvestigacionTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_LineaInvestigacion()
    {
        var lineaInvestigacion = _fixture.SetupLineaInvestigacion();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllLineasInvestigacionQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        lineaInvestigacion.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_LineaInvestigacion()
    {
        _fixture.SetupLineaInvestigacion(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllLineasInvestigacionQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}
