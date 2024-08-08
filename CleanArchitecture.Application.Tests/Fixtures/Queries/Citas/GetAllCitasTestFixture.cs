using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.Citas.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Citas;

public sealed class GetAllCitasTestFixture : QueryHandlerBaseFixture
{
    public GetAllCitasQueryHandler QueryHandler { get; }
    private ICitaRepository CitaRepository { get; }

    public GetAllCitasTestFixture()
    {
        CitaRepository = Substitute.For<ICitaRepository>();
        var sortingProvider = new CitaViewModelSortProvider();

        QueryHandler = new GetAllCitasQueryHandler(CitaRepository, sortingProvider);
    }

    public Cita SetupCita(bool deleted = false)
    {
        var cita = new Cita(Guid.NewGuid(), "123", Guid.NewGuid(), Guid.NewGuid(), default, default, default);

        if (deleted)
        {
            cita.Delete();
        }

        var citaList = new List<Cita> { cita }.BuildMock();
        CitaRepository.GetAllNoTracking().Returns(citaList);

        return cita;
    }
}