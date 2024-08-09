using System;
using CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Settings;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.CreateSolicitud;

public sealed class CreateSolicitudCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateSolicitudCommandHandler CommandHandler { get; }

    public CreateSolicitudCommandTestFixture()
    {
        CommandHandler = new CreateSolicitudCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            SolicitudRepository,
            UserRepository,
            AsesoriaSettings,
            UserNotifications,
            User);
    }

    public void SetupExistingSolicitud(Guid id)
    {
        SolicitudRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}