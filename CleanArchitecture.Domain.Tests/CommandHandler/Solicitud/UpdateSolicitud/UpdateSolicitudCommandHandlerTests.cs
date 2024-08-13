using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Solicitud;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.UpdateSolicitud;

public sealed class UpdateSolicitudCommandHandlerTests
{
    private readonly UpdateSolicitudCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Update_Solicitud()
    {
        _fixture.SetupCurrentUser(UserRole.Admin);
        var command = new UpdateSolicitudCommand(
                Guid.NewGuid(),
                SolicitudStatus.Rechazado);

        _fixture.SetupExistingSolicitud(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        var solicitudEstado = (int)command.Estado;
        _fixture
            .VerifyCommit()
            .VerifyNoDomainNotification()
            .VerifyRaisedEvent<SolicitudUpdatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.Estado == solicitudEstado);
    }

    [Fact]
    public async Task Should_Not_Update_Solicitud_Insufficient_Permissions()
    {
        var command = new UpdateSolicitudCommand(
            Guid.NewGuid(),
            SolicitudStatus.Rechazado);

        _fixture.SetupUser();

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<SolicitudUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to update solicitud {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Update_Solicitud_Not_Existing()
    {
        var command = new UpdateSolicitudCommand(
            Guid.NewGuid(),
            SolicitudStatus.Rechazado);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<SolicitudUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no solicitud with Id {command.AggregateId}");
    }
}