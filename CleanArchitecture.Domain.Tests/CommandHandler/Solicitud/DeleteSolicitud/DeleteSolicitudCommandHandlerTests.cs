using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Solicitudes.DeleteSolicitud;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Solicitud;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.DeleteSolicitud;

public sealed class DeleteSolicitudCommandHandlerTests
{
    private readonly DeleteSolicitudCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Delete_Solicitud()
    {
        var solicitud = _fixture.SetupSolicitud();

        var command = new DeleteSolicitudCommand(solicitud.Id);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<SolicitudDeletedEvent>(x => x.AggregateId == solicitud.Id);
    }

    [Fact]
    public async Task Should_Not_Delete_Non_Existing_Solicitud()
    {
        _fixture.SetupSolicitud();

        var command = new DeleteSolicitudCommand(Guid.NewGuid());

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<SolicitudDeletedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no solicitud with Id {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Delete_Solicitud_Insufficient_Permissions()
    {
        var solicitud = _fixture.SetupSolicitud();
        _fixture.SetupUser();

        var command = new DeleteSolicitudCommand(solicitud.Id);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<SolicitudDeletedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to delete solicitud {command.AggregateId}");
    }
}