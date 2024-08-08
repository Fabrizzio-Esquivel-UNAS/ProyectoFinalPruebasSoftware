using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.IntegrationTests.Fixtures;

public sealed class CitaTestFixture : TestFixtureBase
{
    public Guid CreatedCitaId { get; } = Guid.NewGuid();
    public Guid CreatedTenantId { get; } = Guid.NewGuid();
    public Guid CreatedFacultadId { get; } = Guid.NewGuid();
    public Guid CreatedEscuelaId { get; } = Guid.NewGuid();
    public Guid CreatedUserId { get; } = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);

        context.Tenants.Add(new Tenant(
            CreatedTenantId,
            "Test Tenant"));

        context.Users.Add(new User(
            CreatedUserId,
            CreatedTenantId,
            Guid.NewGuid(),
            Guid.NewGuid(),
            "test@user.de",
            "test",
            "user",
            "Test User",
            "123456789",
            "0123456789",
            UserRole.User));

        context.Citas.Add(new Cita(
            CreatedCitaId,
            "",
            CreatedUserId,
            CreatedUserId,
            default,
            default,
            default));

        context.SaveChanges();
    }
}