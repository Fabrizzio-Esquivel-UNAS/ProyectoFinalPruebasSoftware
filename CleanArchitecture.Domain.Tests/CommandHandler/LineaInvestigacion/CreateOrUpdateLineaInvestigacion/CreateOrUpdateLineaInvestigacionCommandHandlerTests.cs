using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.LineasInvestigacion.CreateLineaInvestigacion;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.LineaInvestigacion;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.LineaInvestigacion.CreateLineaInvestigacion;

public sealed class CreateOrUpdateLineaInvestigacionCommandHandlerTests
{
    private readonly CreateOrUpdateLineaInvestigacionCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_LineaInvestigacion()
    {
        var lineaInvestigacion = _fixture.SetupLineaInvestigacion();

        var command = new CreateLineaInvestigacionCommand(
            Guid.NewGuid(),
            lineaInvestigacion.FacultadId,
            lineaInvestigacion.GrupoInvestigacionId,
            lineaInvestigacion.Nombre);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<LineaInvestigacionCreatedEvent>(x => x.AggregateId == command.AggregateId);
    }

    [Fact]
    public async Task Should_Not_Create_Already_Existing_LineaInvestigacion()
    {
        var command = new CreateLineaInvestigacionCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Nombre de la Línea de Investigación");

        _fixture.SetupExistingLineaInvestigacion(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<LineaInvestigacionCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.LineaInvestigacion.AlreadyExists,
                $"There is already a lineainvestigacion with Id {command.AggregateId}");
    }
}

