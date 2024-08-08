using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.GruposInvestigacion.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.GruposInvestigacion;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.GruposInvestigacion;

public sealed class GetAllGruposInvestigacionQueryHandlerTests
{
    private readonly GetAllGruposInvestigacionTestFixture _fixture = new();

 
    [Fact]
    public async Task Should_Get_Existing_GrupoInvestigacion()
    {
        var grupoInvestigacion = _fixture.SetupGrupoInvestigacion();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllGruposInvestigacionQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        grupoInvestigacion.Should().BeEquivalentTo(result.Items.First(), options => options
            .Excluding(x => x.CoordinadorUserId) // Excluir CoordinadorUserId de la comparación
            .ExcludingMissingMembers()); // Excluir cualquier miembro faltante
    }


    [Fact]
    public async Task Should_Not_Get_Deleted_GrupoInvestigacion()
    {
        _fixture.SetupGrupoInvestigacion(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new GetAllGruposInvestigacionQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}
