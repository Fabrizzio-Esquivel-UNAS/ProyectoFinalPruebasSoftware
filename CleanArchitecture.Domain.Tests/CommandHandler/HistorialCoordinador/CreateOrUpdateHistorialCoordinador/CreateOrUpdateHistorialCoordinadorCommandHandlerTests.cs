using System.Linq;
using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.CreateOrUpdateHistorialCoordinador;
using CleanArchitecture.Shared.Events.HistorialCoordinador;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.CreateHistorialCoordinador;

public sealed class CreateOrUpdateHistorialCoordinadorCommandHandlerTests
{
    private readonly CreateOrUpdateHistorialCoordinadorCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_HistorialCoordinador()
    {
        var historialCoordinador = _fixture.SetupHistorialCoordinador();

        var command = new CreateHistorialCoordinadorCommand(
            Guid.NewGuid(),
            historialCoordinador.UserId,
            historialCoordinador.GrupoInvestigacionId,
            DateTime.UtcNow);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<HistorialCoordinadorCreatedEvent>(x => x.AggregateId == command.AggregateId);
    }

    [Fact]
    public async Task Should_Not_Create_Already_Existing_HistorialCoordinador()
    {
        var command = new CreateHistorialCoordinadorCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow);

        _fixture.SetupExistingHistorialCoordinador(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<HistorialCoordinadorCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.HistorialCoordinador.AlreadyExists,
                $"There is already a historialcoordinador with Id {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Create_HistorialCoordinador_If_User_Already_Coordinador_Of_Other_GrupoInvestigacion()
    {
        var existingCoordinador = _fixture.SetupExistingCoordinadorForDifferentGrupo();

        var command = new CreateHistorialCoordinadorCommand(
            Guid.NewGuid(),
            existingCoordinador.UserId,
            Guid.NewGuid(),
            DateTime.UtcNow);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<HistorialCoordinadorCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.HistorialCoordinador.AlreadyExists,
                $"User with Id {command.UserId} is already an active Coordinador for GrupoInvestigacion with Id {existingCoordinador.GrupoInvestigacionId}");
    }
}
