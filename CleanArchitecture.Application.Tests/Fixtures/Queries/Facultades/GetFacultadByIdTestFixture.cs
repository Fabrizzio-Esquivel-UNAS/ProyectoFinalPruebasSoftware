using System;
using CleanArchitecture.Application.Queries.Facultades.GetFacultadesById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Facultades;

public sealed class GetFacultadByIdTestFixture : QueryHandlerBaseFixture
{
    public GetFacultadesByIdQueryHandler QueryHandler { get; }
    private IFacultadRepository FacultadRepository { get; }

    public GetFacultadByIdTestFixture()
    {
        FacultadRepository = Substitute.For<IFacultadRepository>();

        QueryHandler = new GetFacultadesByIdQueryHandler(
            FacultadRepository,
            Bus);
    }

    public Facultad SetupFacultad(bool deleted = false)
    {
        var facultad = new Facultad(Guid.NewGuid(), "Nombre de la Facultad");

        if (deleted)
        {
            facultad.Delete();
        }
        else
        {
            FacultadRepository.GetByIdAsync(Arg.Is<Guid>(y => y == facultad.Id)).Returns(facultad);
        }

        return facultad;
    }
}
