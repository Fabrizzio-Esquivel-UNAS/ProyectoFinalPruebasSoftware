using System;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Commands.Citas.UpdateCita;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Settings;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.UpdateCita;

public sealed class UpdateCitaCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateCitaCommandHandler CommandHandler { get; }
    public ICitaRepository CitaRepository { get; }
    public ISolicitudRepository SolicitudRepository { get; }
    public IContratoRepository ContratoRepository { get; }
    public IUserRepository UserRepository { get; }

    public IOptionsSnapshot<AsesoriaSettings> AsesoriaSettings;

    public UpdateCitaCommandTestFixture()
    {
        CitaRepository = Substitute.For<ICitaRepository>();
        SolicitudRepository = Substitute.For<ISolicitudRepository>();
        ContratoRepository = Substitute.For<IContratoRepository>();
        UserRepository = Substitute.For<IUserRepository>();
        AsesoriaSettings = Substitute.For<IOptionsSnapshot<AsesoriaSettings>>();

        CommandHandler = new UpdateCitaCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            CitaRepository,
            SolicitudRepository,
            ContratoRepository,
            UserRepository,
            AsesoriaSettings,
            User);
    }

    public Entities.Cita SetupCita()
    {
        var cita = new Entities.Cita(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            default,
            default,
            default);

        CitaRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == cita.Id))
            .Returns(cita);

        return cita;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingCita(Guid id)
    {
        CitaRepository
            .GetByIdAsync(Arg.Is<Guid>(x => x == id))
            .Returns(new Entities.Cita(id, Guid.NewGuid().ToString(), Guid.NewGuid(), Guid.NewGuid(), default, default, default));
    }
}