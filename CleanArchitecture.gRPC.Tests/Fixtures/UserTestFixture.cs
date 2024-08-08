using System;
using System.Collections.Generic;
using CleanArchitecture.Application.gRPC;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.gRPC.Tests.Fixtures;

public sealed class UserTestFixture
{
    private IUserRepository UserRepository { get; } = Substitute.For<IUserRepository>();

    public UsersApiImplementation UsersApiImplementation { get; }

    public IEnumerable<User> ExistingUsers { get; }

    public UserTestFixture()
    {
        ExistingUsers = new List<User>
        {
            new(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "test@test.de",
                "Test First Name",
                "Test Last Name",
                "Test Password",
                "111111111",
                "1111111111",
                UserRole.User),
            new(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "email@Email.de",
                "Email First Name",
                "Email Last Name",
                "Email Password",
                "222222222",
                "2222222222",
                UserRole.Admin),
            new(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "user@user.de",
                "User First Name",
                "User Last Name",
                "User Password",
                "333333333",
                "3333333333",
                UserRole.User)
        };

        var queryable = ExistingUsers.BuildMock();

        UserRepository.GetAllNoTracking().Returns(queryable);

        UsersApiImplementation = new UsersApiImplementation(UserRepository);
    }
}