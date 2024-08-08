using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Solicitud;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.CreateSolicitud;

public sealed class CreateSolicitudCommandHandlerTests
{
    private readonly CreateSolicitudCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_Solicitud()
    {
        _fixture.SetupCurrentUser();
        var user = _fixture.SetupUserWithRole(Enums.UserRole.User);
        var asesorUser = _fixture.SetupUserWithRole(Enums.UserRole.Asesor);
        var command = new CreateSolicitudCommand(
            Guid.NewGuid(),
            user.Id,
            asesorUser.Id,
            1,
            "Test");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<SolicitudCreatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.AsesorUserId == command.AsesorUserId &&
                x.AsesoradoUserId == command.AsesoradoUserId &&
                x.NumeroTesis == command.NumeroTesis &&
                x.Mensaje == command.Mensaje);
    }

    [Fact]
    public async Task Should_Not_Create_Solicitud_Insufficient_Permissions()
    {
        _fixture.SetupUserAdmin();

        var command = new CreateSolicitudCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            1,
            "Test");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<SolicitudCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to create solicitud {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Create_Solicitud_Already_Exists()
    {
        var command = new CreateSolicitudCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            1,
            "Test");

        _fixture.SetupExistingSolicitud(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<SolicitudCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Solicitud.AlreadyExists,
                $"There is already a solicitud with Id {command.AggregateId}");
    }
}