using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.Facultades.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Facultades;

public sealed class GetAllFacultadesTestFixture : QueryHandlerBaseFixture
{
    public GetAllFacultadesQueryHandler QueryHandler { get; }
    private IFacultadRepository FacultadRepository { get; }

    public GetAllFacultadesTestFixture()
    {
        FacultadRepository = Substitute.For<IFacultadRepository>();
        var sortingProvider = new FacultadViewModelSortProvider();

        QueryHandler = new GetAllFacultadesQueryHandler(FacultadRepository, sortingProvider);
    }

    public Facultad SetupFacultad(bool deleted = false)
    {
        var facultad = new Facultad(Guid.NewGuid(), "Nombre de la Facultad");

        if (deleted)
        {
            facultad.Delete();
        }

        var facultadList = new List<Facultad> { facultad }.BuildMock();
        FacultadRepository.GetAllNoTracking().Returns(facultadList);

        return facultad;
    }
}

