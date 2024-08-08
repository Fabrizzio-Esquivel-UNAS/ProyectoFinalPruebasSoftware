using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Facultades.GetFacultadesById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Facultades;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Facultades;

public sealed class GetFacultadByIdQueryHandlerTests
{
    private readonly GetFacultadByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Facultad()
    {
        var facultad = _fixture.SetupFacultad();

        var result = await _fixture.QueryHandler.Handle(
            new GetFacultadesByIdQuery(facultad.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        facultad.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Facultad()
    {
        var facultad = _fixture.SetupFacultad(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetFacultadesByIdQuery(facultad.Id),
        default);

        _fixture.VerifyExistingNotification(
            nameof(GetFacultadesByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"Facultad with id {facultad.Id} could not be found");
        result.Should().BeNull();
    }
}
