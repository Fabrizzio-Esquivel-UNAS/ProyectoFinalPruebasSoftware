using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Escuelas.GetEscuelasById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Escuelas;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Escuelas;

public sealed class GetEscuelaByIdQueryHandlerTests
{
    private readonly GetEscuelaByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Escuela()
    {
        var escuela = _fixture.SetupEscuela();

        var result = await _fixture.QueryHandler.Handle(
            new GetEscuelasByIdQuery(escuela.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        escuela.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Escuela()
    {
        var escuela = _fixture.SetupEscuela(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetEscuelasByIdQuery(escuela.Id),
        default);

        _fixture.VerifyExistingNotification(
            nameof(GetEscuelasByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"Escuela with id {escuela.Id} could not be found");
        result.Should().BeNull();
    }
}
