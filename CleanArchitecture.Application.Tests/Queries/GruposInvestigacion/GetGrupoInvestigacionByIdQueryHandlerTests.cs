using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.GruposInvestigacion.GetGrupoInvestigacionById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.GruposInvestigacion;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.GruposInvestigacion;

public sealed class GetGrupoInvestigacionByIdQueryHandlerTests
{
    private readonly GetGrupoInvestigacionByIdTestFixture _fixture = new();

    /*[Fact]
     public async Task Should_Get_Existing_GrupoInvestigacion()
    {
        var grupoInvestigacion = _fixture.SetupGrupoInvestigacion();

        var result = await _fixture.QueryHandler.Handle(
            new GetGrupoInvestigacionByIdQuery(grupoInvestigacion.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        grupoInvestigacion.Should().BeEquivalentTo(result);
    }*/

    [Fact]
    public async Task Should_Not_Get_Deleted_GrupoInvestigacion()
    {
        var grupoInvestigacion = _fixture.SetupGrupoInvestigacion(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetGrupoInvestigacionByIdQuery(grupoInvestigacion.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetGrupoInvestigacionByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"GrupoInvestigacion with id {grupoInvestigacion.Id} could not be found");
        result.Should().BeNull();
    }
}
