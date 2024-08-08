using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Escuelas.CreateEscuela;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Tests.CommandHandler.Escuela.CreateOrUpdateEscuela;
using CleanArchitecture.Shared.Events.Escuela;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Escuela.CreateEscuela;

public sealed class CreateOrUpdateEscuelaCommandHandlerTests
{
    private readonly CreateOrUpdateEscuelaCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_Escuela()
    {
        var escuela = _fixture.SetupEscuela();

        var command = new CreateEscuelaCommand(
            Guid.NewGuid(),
            escuela.FacultadId,
            escuela.Nombre);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<EscuelaCreatedEvent>(x => x.AggregateId == command.AggregateId);
    }

    [Fact]
    public async Task Should_Not_Create_Already_Existing_Escuela()
    {
        var command = new CreateEscuelaCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Nombre de la Escuela");

        _fixture.SetupExistingEscuela(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<EscuelaCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Escuela.AlreadyExists,
                $"There is already a escuela with Id {command.AggregateId}");
    }
}
