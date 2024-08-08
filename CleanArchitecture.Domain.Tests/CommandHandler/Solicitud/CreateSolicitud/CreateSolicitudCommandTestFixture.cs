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

    private ISolicitudRepository SolicitudRepository { get; }
    private IUserRepository UserRepository { get; }
    private IOptionsSnapshot<AsesoriaSettings> AsesoriaSettings { get; }
    private IUserNotifications UserNotifications { get; }

    public CreateSolicitudCommandTestFixture()
    {
        SolicitudRepository = Substitute.For<ISolicitudRepository>();
        UserRepository = Substitute.For<IUserRepository>();
        AsesoriaSettings = Substitute.For<IOptionsSnapshot<AsesoriaSettings>>();
        UserNotifications = Substitute.For<IUserNotifications>();

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

    public void SetupCurrentUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public Entities.User SetupUserWithRole(UserRole role)
    {
        var user = new Entities.User(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "Password",
            "123456789",
            "0123456789",
            role);

        UserRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == user.Id))
            .Returns(user);

        return user;
    }

    public void SetupUserAdmin()
    {
        User.GetUserRole().Returns(UserRole.Admin);
    }

    public void SetupExistingSolicitud(Guid id)
    {
        SolicitudRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}