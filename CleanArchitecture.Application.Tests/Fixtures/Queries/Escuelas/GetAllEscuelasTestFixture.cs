using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.Escuelas.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Escuelas;

public sealed class GetAllEscuelasTestFixture : QueryHandlerBaseFixture
{
    public GetAllEscuelasQueryHandler QueryHandler { get; }
    private IEscuelaRepository EscuelaRepository { get; }

    public GetAllEscuelasTestFixture()
    {
        EscuelaRepository = Substitute.For<IEscuelaRepository>();
        var sortingProvider = new EscuelaViewModelSortProvider();

        QueryHandler = new GetAllEscuelasQueryHandler(EscuelaRepository, sortingProvider);
    }

    public Escuela SetupEscuela(bool deleted = false)
    {
        var escuela = new Escuela(Guid.NewGuid(), Guid.NewGuid(), "Nombre de la Escuela");

        if (deleted)
        {
            escuela.Delete();
        }

        var escuelaList = new List<Escuela> { escuela }.BuildMock();
        EscuelaRepository.GetAllNoTracking().Returns(escuelaList);

        return escuela;
    }
}
