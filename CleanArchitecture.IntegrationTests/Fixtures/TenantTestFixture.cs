using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.IntegrationTests.Fixtures;

public sealed class TenantTestFixture : TestFixtureBase
{
    public Guid CreatedTenantId { get; } = Guid.NewGuid();
    public Guid CreatedFacultadId { get; } = Guid.NewGuid();
    public Guid CreatedEscuelaId { get; } = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);

        context.Tenants.Add(new Tenant(
            CreatedTenantId,
            "Test Tenant"));

        context.Facultades.Add(new Facultad(
            CreatedFacultadId,
            "Test Facultad"));

        context.Escuelas.Add(new Escuela(
            CreatedEscuelaId,
            CreatedFacultadId,
            "Test Escuela"));

        context.Users.Add(new User(
            Guid.NewGuid(),
            CreatedTenantId,
            Guid.NewGuid(),
            CreatedEscuelaId,
            "test@user.de",
            "test",
            "user",
            "Test User",
            "123456789",
            "0123456789",
            UserRole.User));

        context.SaveChanges();
    }
}