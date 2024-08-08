using System;
using CleanArchitecture.Application.Queries.Users.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Users;

public sealed class GetAllUsersTestFixture : QueryHandlerBaseFixture
{
    private IUserRepository UserRepository { get; }
    private ISolicitudRepository SolicitudRepository {  get; }
    private IContratoRepository ContratoRepository { get; }
    private IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }
    public GetAllUsersQueryHandler Handler { get; }
    public Guid ExistingUserId { get; } = Guid.NewGuid();

    public GetAllUsersTestFixture()
    {
        UserRepository = Substitute.For<IUserRepository>();
        SolicitudRepository = Substitute.For <ISolicitudRepository>();
        ContratoRepository = Substitute.For<IContratoRepository>();
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();
        var sortingProvider = new UserViewModelSortProvider();

        Handler = new GetAllUsersQueryHandler(
            UserRepository,
            SolicitudRepository,
            ContratoRepository,
            HistorialCoordinadorRepository,
            sortingProvider);
    }

    public User SetupUserAsync()
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

        var query = new[] { user }.BuildMock();

        UserRepository.GetAllNoTracking().Returns(query);

        return user;
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

        var query = new[] { user }.BuildMock();

        UserRepository.GetAllNoTracking().Returns(query);
    }
}