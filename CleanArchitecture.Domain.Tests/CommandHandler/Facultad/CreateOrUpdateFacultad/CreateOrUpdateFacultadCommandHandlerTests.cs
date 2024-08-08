using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Facultades.CreateFacultad;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Tests.CommandHandler.Facultad.CreateOrUpdateFacultad;
using CleanArchitecture.Shared.Events.Facultad;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Facultad.CreateFacultad;

public sealed class CreateOrUpdateFacultadCommandHandlerTests
{
    private readonly CreateOrUpdateFacultadCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_Facultad()
    {
        var facultad = _fixture.SetupFacultad();

        var command = new CreateFacultadCommand(
            Guid.NewGuid(),
            facultad.Nombre);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<FacultadCreatedEvent>(x => x.AggregateId == command.AggregateId);
    }

    [Fact]
    public async Task Should_Not_Create_Already_Existing_Facultad()
    {
        var command = new CreateFacultadCommand(
            Guid.NewGuid(),
            "Nombre de la Facultad");

        _fixture.SetupExistingFacultad(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<FacultadCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Facultad.AlreadyExists,
                $"There is already a facultad with Id {command.AggregateId}");
    }
}
