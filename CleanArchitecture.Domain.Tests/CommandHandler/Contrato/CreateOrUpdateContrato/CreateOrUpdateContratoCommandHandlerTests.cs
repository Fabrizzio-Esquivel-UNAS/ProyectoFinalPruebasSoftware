using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Contratos.CreateContrato;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Contrato;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Contrato.CreateContrato;

public sealed class CreateOrUpdateContratoCommandHandlerTests
{
    private readonly CreateOrUpdateContratoCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_Contrato()
    {
        var contrato = _fixture.SetupContrato();

        var command = new CreateContratoCommand(
            Guid.NewGuid(),
            contrato.SolicitudId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<ContratoCreatedEvent>(x => x.AggregateId == command.ContratoId);
    }

    [Fact]
    public async Task Should_Not_Create_Already_Existing_Contrato()
    {
        var command = new CreateContratoCommand(
            Guid.NewGuid(),
            Guid.NewGuid());

        _fixture.SetupExistingContrato(command.ContratoId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ContratoCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Contrato.AlreadyExists,
                $"There is already a contrato with Id {command.ContratoId}");
    }
}
   
    /*
    [Fact]
    public async Task Should_Not_Create_Contrato_Solicitud_Does_Not_Exist()
    {
        var command = new CreateContratoCommand(
            Guid.NewGuid(),
            Guid.NewGuid()); // Non-existing SolicitudId

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ContratoCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no solicitud with Id {command.SolicitudId}");
    }
}
    */