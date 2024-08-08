using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Database;
using Grpc.Net.Client;

namespace CleanArchitecture.IntegrationTests.Fixtures.gRPC;

public sealed class GetSolicitudesByIdsTestFixture : TestFixtureBase
{
    public GrpcChannel GrpcChannel { get; }
    public Guid CreatedSolicitudId { get; } = Guid.NewGuid();

    public GetSolicitudesByIdsTestFixture()
    {
        GrpcChannel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            HttpHandler = Factory.Server.CreateHandler()
        });
    }

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);

        var tenant = CreateSolicitud();

        context.Solicitudes.Add(tenant);
        context.SaveChanges();
    }

    public Solicitud CreateSolicitud()
    {
        return new Solicitud(
            CreatedSolicitudId,
            Guid.NewGuid(),
            Guid.NewGuid(),
            1,
            "Test Solicitud");
    }
}