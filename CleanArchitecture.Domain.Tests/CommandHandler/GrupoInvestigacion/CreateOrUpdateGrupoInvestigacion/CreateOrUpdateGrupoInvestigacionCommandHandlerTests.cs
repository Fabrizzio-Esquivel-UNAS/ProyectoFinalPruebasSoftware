using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.GruposInvestigacion.CreateGrupoInvestigacion;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Tests.CommandHandler.GrupoInvestigacion.CreateOrUpdateGrupoInvestigacion;
using CleanArchitecture.Shared.Events.GrupoInvestigacion;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.GrupoInvestigacion.CreateGrupoInvestigacion;

public sealed class CreateOrUpdateGrupoInvestigacionCommandHandlerTests
{
    private readonly CreateOrUpdateGrupoInvestigacionCommandTestFixture _fixture = new();

   /* [Fact]
    public async Task Should_Create_GrupoInvestigacion()
    {
        var grupoInvestigacion = _fixture.SetupGrupoInvestigacion();

        var command = new CreateGrupoInvestigacionCommand(
            Guid.NewGuid(),
            grupoInvestigacion.Nombre,
            grupoInvestigacion.HistorialCoordinadores.First().UserId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<GrupoInvestigacionCreatedEvent>(x => x.AggregateId == command.AggregateId);
    }*/

    [Fact]
    public async Task Should_Not_Create_Already_Existing_GrupoInvestigacion()
    {
        var command = new CreateGrupoInvestigacionCommand(
            Guid.NewGuid(),
            "Nombre del Grupo",
            Guid.NewGuid());

        _fixture.SetupExistingGrupoInvestigacion(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<GrupoInvestigacionCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.GrupoInvestigacion.AlreadyExists,
                $"There is already a GrupoInvestigacion with Id {command.AggregateId}");
    }
}
