using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Solicitud;
using Org.BouncyCastle.Asn1.Ocsp;
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
        _fixture.SetupCurrentUser(Enums.UserRole.Admin);

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
        _fixture.SetupCurrentUser();
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

    [Fact]
    public async Task Should_Not_Create_Solicitud_Insufficient_Asesor_Permissions_User()
    {
        _fixture.SetupCurrentUser();
        var user = _fixture.SetupUserWithRole(Enums.UserRole.User);
        var asesorUser = _fixture.SetupUserWithRole(Enums.UserRole.User);
        var command = new CreateSolicitudCommand(
            Guid.NewGuid(),
            user.Id,
            asesorUser.Id,
            1,
            "Test");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<SolicitudCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"The user with Id {command.AsesorUserId} does not have the permissions of 'Asesor' role");
    }

    [Fact]
    public async Task Should_Not_Create_Solicitud_Insufficient_Asesor_Permissions_Asesorado()
    {
        _fixture.SetupCurrentUser();
        var user = _fixture.SetupUserWithRole(Enums.UserRole.User);
        var asesorUser = _fixture.SetupUserWithRole(Enums.UserRole.Asesorado);
        var command = new CreateSolicitudCommand(
            Guid.NewGuid(),
            user.Id,
            asesorUser.Id,
            1,
            "Test");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<SolicitudCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"The user with Id {command.AsesorUserId} does not have the permissions of 'Asesor' role");
    }

    [Fact]
    public async Task Should_Create_Solicitud_Asesor_Permissions_Asesor()
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
    public async Task Should_Create_Solicitud_Asesor_Permissions_Coordinador()
    {
        _fixture.SetupCurrentUser();
        var user = _fixture.SetupUserWithRole(Enums.UserRole.User);
        var asesorUser = _fixture.SetupUserWithRole(Enums.UserRole.Coordinador);
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
    public async Task Should_Create_Solicitud_Asesor_Permissions_Admin()
    {
        _fixture.SetupCurrentUser();
        var user = _fixture.SetupUserWithRole(Enums.UserRole.User);
        var asesorUser = _fixture.SetupUserWithRole(Enums.UserRole.Admin);
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
}