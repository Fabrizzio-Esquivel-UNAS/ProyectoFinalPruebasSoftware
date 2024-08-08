using System;
using CleanArchitecture.Domain.Commands.Users.UpdateUser;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.User.UpdateUser;

public sealed class UpdateUserCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateUserCommandHandler CommandHandler { get; }
    public IUserRepository UserRepository { get; }
    private ITenantRepository TenantRepository { get; }
    private IContratoRepository ContratoRepository { get; }
    private ISolicitudRepository SolicitudRepository { get; }
    public IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public UpdateUserCommandTestFixture()
    {
        UserRepository = Substitute.For<IUserRepository>();
        TenantRepository = Substitute.For<ITenantRepository>();
        ContratoRepository = Substitute.For<IContratoRepository>();
        SolicitudRepository = Substitute.For<ISolicitudRepository>();
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();

        CommandHandler = new UpdateUserCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            UserRepository,
            User,
            TenantRepository,
            ContratoRepository,
            SolicitudRepository,
            HistorialCoordinadorRepository);
    }

    public Entities.User SetupUser()
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
            UserRole.User);

        UserRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == user.Id))
            .Returns(user);

        return user;
    }

    public Entities.Tenant SetupTenant(Guid tenantId)
    {
        var tenant = new Entities.Tenant(tenantId, "Name");

        TenantRepository
            .ExistsAsync(Arg.Is<Guid>(y => y == tenant.Id))
            .Returns(true);

        return tenant;
    }

    public void SetupCurrentUser(Guid userId)
    {
        User.GetUserId().Returns(userId);
        User.GetUserRole().Returns(UserRole.User);
    }
}