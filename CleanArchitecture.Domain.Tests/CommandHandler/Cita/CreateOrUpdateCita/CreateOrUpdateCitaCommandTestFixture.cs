using System;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.CreateCita;

public sealed class CreateOrUpdateCitaCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateOrUpdateCitaCommandHandler CommandHandler { get; }

    public CreateOrUpdateCitaCommandTestFixture()
    {
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

    public void SetupExistingCita(Guid id)
    {
        CitaRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}