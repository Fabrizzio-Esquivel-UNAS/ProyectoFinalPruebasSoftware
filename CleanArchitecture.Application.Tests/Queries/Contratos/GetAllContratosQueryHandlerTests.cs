using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Contratos.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Contratos;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Contratos;

public sealed class GetAllContratosQueryHandlerTests
{
    private readonly GetAllContratosTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Contrato()
    {
        var contrato = _fixture.SetupContrato();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllContratosQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        contrato.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Contrato()
    {
        _fixture.SetupContrato(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllContratosQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}
