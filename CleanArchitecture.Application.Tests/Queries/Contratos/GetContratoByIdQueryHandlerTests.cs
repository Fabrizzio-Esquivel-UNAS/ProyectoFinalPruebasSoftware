using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Contratos.GetContratoById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Contratos;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Contratos;

public sealed class GetContratoByIdQueryHandlerTests
{
    private readonly GetContratoByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Contrato()
    {
        var contrato = _fixture.SetupContrato();

        var result = await _fixture.QueryHandler.Handle(
            new GetContratoByIdQuery(contrato.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        contrato.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Contrato()
    {
        var contrato = _fixture.SetupContrato(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetContratoByIdQuery(contrato.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetContratoByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"Contrato with id {contrato.Id} could not be found");
        result.Should().BeNull();
    }
}
