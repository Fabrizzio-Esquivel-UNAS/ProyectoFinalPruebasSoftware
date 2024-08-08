using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Citas.GetCitaById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Citas;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Citas;

public sealed class GetCitaByIdQueryHandlerTests
{
    private readonly GetCitaByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Cita()
    {
        var cita = _fixture.SetupCita();

        var result = await _fixture.QueryHandler.Handle(
            new GetCitaByIdQuery(cita.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        cita.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Cita()
    {
        var cita = _fixture.SetupCita(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetCitaByIdQuery(cita.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetCitaByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"Cita with id {cita.Id} could not be found");
        result.Should().BeNull();
    }
}