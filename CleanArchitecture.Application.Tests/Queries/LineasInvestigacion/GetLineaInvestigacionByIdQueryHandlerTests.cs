using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.LineasInvestigacion.GetLineaInvestigacionById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.LineasInvestigacion;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.LineasInvestigacion;

public sealed class GetLineaInvestigacionByIdQueryHandlerTests
{
    private readonly GetLineaInvestigacionByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_LineaInvestigacion()
    {
        var lineaInvestigacion = _fixture.SetupLineaInvestigacion();

        var result = await _fixture.QueryHandler.Handle(
            new GetLineaInvestigacionByIdQuery(lineaInvestigacion.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        lineaInvestigacion.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_LineaInvestigacion()
    {
        var lineaInvestigacion = _fixture.SetupLineaInvestigacion(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetLineaInvestigacionByIdQuery(lineaInvestigacion.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetLineaInvestigacionByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"LineaInvestigacion with id {lineaInvestigacion.Id} could not be found");
        result.Should().BeNull();
    }
}
