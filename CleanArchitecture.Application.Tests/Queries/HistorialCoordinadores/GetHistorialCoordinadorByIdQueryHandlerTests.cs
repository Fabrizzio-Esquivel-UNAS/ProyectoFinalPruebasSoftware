using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.HistorialCoordinadores.GetHistorialCoordinadorById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.HistorialCoordinadores;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.HistorialCoordinadores;

public sealed class GetHistorialCoordinadorByIdQueryHandlerTests
{
    private readonly GetHistorialCoordinadorByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_HistorialCoordinador()
    {
        var historialCoordinador = _fixture.SetupHistorialCoordinador();

        var result = await _fixture.QueryHandler.Handle(
            new GetHistorialCoordinadorByIdQuery(historialCoordinador.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        historialCoordinador.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_HistorialCoordinador()
    {
        var historialCoordinador = _fixture.SetupHistorialCoordinador(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetHistorialCoordinadorByIdQuery(historialCoordinador.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetHistorialCoordinadorByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"HistorialCoordinador with id {historialCoordinador.Id} could not be found");
        result.Should().BeNull();
    }
}
