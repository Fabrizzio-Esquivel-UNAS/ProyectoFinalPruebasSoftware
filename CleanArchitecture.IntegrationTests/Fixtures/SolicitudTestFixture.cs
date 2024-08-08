using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.IntegrationTests.Fixtures;

public sealed class SolicitudTestFixture : TestFixtureBase
{
    public Guid CreatedSolicitudId { get; } = Guid.NewGuid();
    public Guid CreatedFacultadId { get; } = Guid.NewGuid();
    public Guid CreatedEscuelaId { get; } = Guid.NewGuid();
    public Guid CreateAsesoradoUserId { get; } = Guid.NewGuid();
    public Guid CreatedAsesorUserId { get; } = Guid.NewGuid();
    public Guid CreatedLineaInvestigacionId { get; } = Guid.NewGuid();
    public Guid CreatedGrupoInvestigacionId { get; } = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);

        context.Solicitudes.Add(new Solicitud(
            CreatedSolicitudId,
            CreateAsesoradoUserId,
            CreatedAsesorUserId,
            1,
            "Test Solicitud"));

        context.Facultades.Add(new Facultad(
            CreatedFacultadId,
            "Test Facultad"));

        context.Escuelas.Add(new Escuela(
            CreatedEscuelaId,
            CreatedFacultadId,
            "Test Escuela"));

        context.Users.Add(new User(
            CreatedAsesorUserId,
            CreatedSolicitudId,
            CreatedLineaInvestigacionId,
            CreatedEscuelaId,
            "testAsesor@user.de",
            "testAsesor",
            "user",
            "Test User",
            "123456789",
            "0123456789",
            UserRole.Asesor));

        context.SaveChanges();
    }
}