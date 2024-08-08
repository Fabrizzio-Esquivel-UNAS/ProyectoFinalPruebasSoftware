using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.HistorialCoordinadores.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.HistorialCoordinadores;

public sealed class GetAllHistorialCoordinadoresTestFixture : QueryHandlerBaseFixture
{
    public GetAllHistorialCoordinadoresQueryHandler QueryHandler { get; }
    private IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public GetAllHistorialCoordinadoresTestFixture()
    {
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();
        var sortingProvider = new HistorialCoordinadorViewModelSortProvider();

        QueryHandler = new GetAllHistorialCoordinadoresQueryHandler(HistorialCoordinadorRepository, sortingProvider);
    }

    public HistorialCoordinador SetupHistorialCoordinador(bool deleted = false)
    {
        var historialCoordinador = new HistorialCoordinador(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, null);

        if (deleted)
        {
            historialCoordinador.Delete();
        }

        var historialCoordinadorList = new List<HistorialCoordinador> { historialCoordinador }.BuildMock();
        HistorialCoordinadorRepository.GetAllNoTracking().Returns(historialCoordinadorList);

        return historialCoordinador;
    }
}
