using System;
using System.Linq;
using CleanArchitecture.Application.Queries.Users.GetUserById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Users;

public sealed class GetUserByIdTestFixture : QueryHandlerBaseFixture
{
    private IUserRepository UserRepository { get; }
    private IContratoRepository ContratoRepository { get; }
    private ISolicitudRepository SolicitudRepository { get; }
    public GetUserByIdQueryHandler Handler { get; }
    public Guid ExistingUserId { get; } = Guid.NewGuid();
    public IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public GetUserByIdTestFixture()
    {
        UserRepository = Substitute.For<IUserRepository>();
        ContratoRepository = Substitute.For<IContratoRepository>();
        SolicitudRepository = Substitute.For<ISolicitudRepository>();
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();

        Handler = new GetUserByIdQueryHandler(
            UserRepository, 
            ContratoRepository, 
            SolicitudRepository,
            Bus,
            HistorialCoordinadorRepository);
    }

    public void SetupUserAsync()
    {
        var user = new User(
            ExistingUserId,
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

        UserRepository.GetByIdAsync(Arg.Is<Guid>(y => y == ExistingUserId)).Returns(user);
    }

    public void SetupDeletedUserAsync()
    {
        var user = new User(
            ExistingUserId,
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

        user.Delete();

        var query = new[] { user }.AsQueryable().BuildMock();

        UserRepository.GetAllNoTracking().Returns(query);
    }
}