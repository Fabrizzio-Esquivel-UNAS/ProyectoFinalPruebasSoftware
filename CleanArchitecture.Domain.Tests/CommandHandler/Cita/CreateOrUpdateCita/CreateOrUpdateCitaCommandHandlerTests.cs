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

    [Fact]
    public async Task Should_Create_Cita()
    {
        var asesorUser = _fixture.SetupUserWithRole(UserRole.Asesor);
        var asesoradoUser = _fixture.SetupUserWithRole(UserRole.Asesorado);
        var solicitud = _fixture.SetupSolicitud(asesoradoUser.Id, asesoradoUser.Id);
        var contrato = _fixture.SetupContrato(solicitud.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(30));

        var command = new CreateOrUpdateCitaCommand(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            asesorUser.Id,
            asesoradoUser.Email,
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<CitaCreatedEvent>(x => x.AggregateId == command.AggregateId);
    }

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

    [Fact]
    public async Task Should_Not_Create_Cita_Fecha_Before_Contrato()
    {
        var asesorUser = _fixture.SetupUserWithRole(UserRole.Asesor);
        var asesoradoUser = _fixture.SetupUserWithRole(UserRole.Asesorado);
        var solicitud = _fixture.SetupSolicitud(asesoradoUser.Id, asesoradoUser.Id);
        var contrato = _fixture.SetupContrato(solicitud.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(30));

        var command = new CreateOrUpdateCitaCommand(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            asesorUser.Id,
            asesoradoUser.Email,
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)),
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<CitaCreatedEvent>();
    }

    [Fact]
    public async Task Should_Not_Create_Cita_Fecha_After_Contrato()
    {
        var asesorUser = _fixture.SetupUserWithRole(UserRole.Asesor);
        var asesoradoUser = _fixture.SetupUserWithRole(UserRole.Asesorado);
        var solicitud = _fixture.SetupSolicitud(asesoradoUser.Id, asesoradoUser.Id);
        var contrato = _fixture.SetupContrato(solicitud.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(30));

        var command = new CreateOrUpdateCitaCommand(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            asesorUser.Id,
            asesoradoUser.Email,
            DateTime.UtcNow.AddDays(31),
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<CitaCreatedEvent>();
    }

    [Fact]
    public async Task Should_Create_Cita_Fecha_During_Contracto()
    {
        var asesorUser = _fixture.SetupUserWithRole(UserRole.Asesor);
        var asesoradoUser = _fixture.SetupUserWithRole(UserRole.Asesorado);
        var solicitud = _fixture.SetupSolicitud(asesoradoUser.Id, asesoradoUser.Id);
        var contrato = _fixture.SetupContrato(solicitud.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(30));

        var command = new CreateOrUpdateCitaCommand(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            asesorUser.Id,
            asesoradoUser.Email,
            DateTime.UtcNow.AddDays(15),
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<CitaCreatedEvent>(x => x.AggregateId == command.AggregateId);
    }
}