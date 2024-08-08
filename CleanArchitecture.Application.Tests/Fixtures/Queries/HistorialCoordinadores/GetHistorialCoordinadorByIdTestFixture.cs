using System;
using CleanArchitecture.Application.Queries.HistorialCoordinadores.GetHistorialCoordinadorById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.HistorialCoordinadores;

public sealed class GetHistorialCoordinadorByIdTestFixture : QueryHandlerBaseFixture
{
    public GetHistorialCoordinadorByIdQueryHandler QueryHandler { get; }
    private IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public GetHistorialCoordinadorByIdTestFixture()
    {
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();

        QueryHandler = new GetHistorialCoordinadorByIdQueryHandler(
            HistorialCoordinadorRepository,
            Bus);
    }

    public HistorialCoordinador SetupHistorialCoordinador(bool deleted = false)
    {
        var historialCoordinador = new HistorialCoordinador(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.Now,
            null);

        if (deleted)
        {
            historialCoordinador.Delete();
        }
        else
        {
            HistorialCoordinadorRepository.GetByIdAsync(Arg.Is<Guid>(y => y == historialCoordinador.Id)).Returns(historialCoordinador);
        }

        return historialCoordinador;
    }
}
