using System;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Database;
using Grpc.Net.Client;

namespace CleanArchitecture.IntegrationTests.Fixtures.gRPC;

public sealed class GetCitasByIdsTestFixture : TestFixtureBase
{
    public GrpcChannel GrpcChannel { get; }
    public Guid CreatedCitaId { get; } = Guid.NewGuid();

    public GetCitasByIdsTestFixture()
    {
        GrpcChannel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            HttpHandler = Factory.Server.CreateHandler()
        });
    }

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);

        var cita = CreateCita();

        context.Citas.Add(cita);
        context.SaveChanges();
    }

    public Cita CreateCita()
    {
        return new Cita(
            CreatedCitaId,
            "123",
            Guid.NewGuid(),
            Guid.NewGuid(),
            default(DateTime),
            default(DateTime),
            default(DateTime));
    }
}