using System;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.CreateCita;

public sealed class CreateOrUpdateCitaCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateOrUpdateCitaCommandHandler CommandHandler { get; }
    public ICitaRepository CitaRepository { get; }
    private IUserRepository UserRepository { get; }
    public ISolicitudRepository SolicitudRepository { get; }
    public IContratoRepository ContratoRepository { get; }

    public CreateOrUpdateCitaCommandTestFixture()
    {
        CitaRepository = Substitute.For<ICitaRepository>();
        UserRepository = Substitute.For<IUserRepository>();
        SolicitudRepository = Substitute.For<ISolicitudRepository>();
        ContratoRepository = Substitute.For<IContratoRepository>();

        CommandHandler = new CreateOrUpdateCitaCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            CitaRepository,
            UserRepository,
            SolicitudRepository,
            ContratoRepository,
            User);
    }

    public Entities.Cita SetupCita()
    {
        var cita = new Entities.Cita(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            default(DateTime),
            default(DateTime),
            default(DateTime));

        CitaRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == cita.Id))
            .Returns(cita);

        return cita;
    }

    public Entities.User SetupUser(Guid userId, UserRole userRole, string userEmail)
    {
        var user = new Entities.User(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            userEmail,
            "Max",
            "Mustermann",
            "Password",
            "123456789",
            "0123456789",
            userRole);

        UserRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == userId))
            .Returns(user);

        return user;
    }

    public void SetupExistingCita(Guid id)
    {
        CitaRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}