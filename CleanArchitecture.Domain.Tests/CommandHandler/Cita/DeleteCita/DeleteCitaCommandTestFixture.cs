using System;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Commands.Citas.DeleteCita;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.DeleteCita;

public sealed class DeleteCitaCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteCitaCommandHandler CommandHandler { get; }
    public ICitaRepository CitaRepository { get; }
    private IUserRepository UserRepository { get; }

    public DeleteCitaCommandTestFixture()
    {
        CitaRepository = Substitute.For<ICitaRepository>();
        UserRepository = Substitute.For<IUserRepository>();

        CommandHandler = new DeleteCitaCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            CitaRepository,
            UserRepository,
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
            default,
            CitaEstado.Inasistido);

        CitaRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == cita.Id))
            .Returns(cita);

        return cita;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
}