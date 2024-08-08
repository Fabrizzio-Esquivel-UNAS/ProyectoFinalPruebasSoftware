using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Commands.Tenants.CreateTenant;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Cita;
using CleanArchitecture.Shared.Events.Tenant;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.CreateCita;

public sealed class CreateOrUpdateCitaCommandHandlerTests
{
    private readonly CreateOrUpdateCitaCommandTestFixture _fixture = new();

    /*
    [Fact]
    public async Task Should_Create_Cita()
    {
        var cita = _fixture.SetupCita();
        _fixture.SetupUserWithRole(cita.AsesorUserId, UserRole.Asesor);
        _fixture.SetupUserWithRole(cita.AsesoradoUserId, UserRole.Asesorado);

        var command = new CreateCitaCommand(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            cita.AsesorUserId,
            cita.AsesoradoUserId,
            CitaEstado.Inasistido);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<CitaCreatedEvent>(x => x.AggregateId == command.AggregateId);
    }
    */

    [Fact]
    public async Task Should_Not_Create_Already_Existing_Cita()
    {
        var command = new CreateOrUpdateCitaCommand(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid(),
            "max@mustermann.com",
            default(DateTime),
            default(DateTime),
            default(DateTime));

        _fixture.SetupExistingCita(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<CitaCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Cita.AlreadyExists,
                $"There is already a cita with Id {command.AggregateId}");
    }
    /*
    [Fact]
    public async Task Should_Not_Create_Cita_Asesor_Does_Not_Exist()
    {
        var cita = _fixture.SetupCita();
        var user = _fixture.SetupUser(cita.AsesoradoUserId, UserRole.Asesorado, "max@mustermann.com");

        var command = new CreateCitaCommand(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid(),
            user.Email,
            CitaEstado.Inasistido);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<CitaCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no user with Id {command.AsesorUserId}");
    }

    [Fact]
    public async Task Should_Not_Create_Cita_Asesorado_Does_Not_Exist()
    {
        var cita = _fixture.SetupCita();
        var user = _fixture.SetupUser(cita.AsesoradoUserId, UserRole.Asesorado, "max@mustermann.com");

        var command = new CreateCitaCommand(
            cita.Id,
            Guid.NewGuid().ToString(),
            Guid.NewGuid(),
            cita.AsesoradoEmail,
            CitaEstado.Inasistido);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<CitaCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no user with Email {command.AsesoradoEmail}");
    }
    */
}