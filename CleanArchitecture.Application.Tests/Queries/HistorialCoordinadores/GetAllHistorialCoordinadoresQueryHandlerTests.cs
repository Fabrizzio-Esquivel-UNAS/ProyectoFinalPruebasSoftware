using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.HistorialCoordinadores.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.HistorialCoordinadores;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.HistorialCoordinadores;

public sealed class GetAllHistorialCoordinadoresQueryHandlerTests
{
    private readonly GetAllHistorialCoordinadoresTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_HistorialCoordinador()
    {
        var historialCoordinador = _fixture.SetupHistorialCoordinador();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllHistorialCoordinadoresQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        historialCoordinador.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_HistorialCoordinador()
    {
        _fixture.SetupHistorialCoordinador(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllHistorialCoordinadoresQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}
