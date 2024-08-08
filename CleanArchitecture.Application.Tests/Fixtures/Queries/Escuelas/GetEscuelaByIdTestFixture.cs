using System;
using CleanArchitecture.Application.Queries.Escuelas.GetEscuelasById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Escuelas;

public sealed class GetEscuelaByIdTestFixture : QueryHandlerBaseFixture
{
    public GetEscuelasByIdQueryHandler QueryHandler { get; }
    private IEscuelaRepository EscuelaRepository { get; }

    public GetEscuelaByIdTestFixture()
    {
        EscuelaRepository = Substitute.For<IEscuelaRepository>();

        QueryHandler = new GetEscuelasByIdQueryHandler(
            EscuelaRepository,
            Bus);
    }

    public Escuela SetupEscuela(bool deleted = false)
    {
        var escuela = new Escuela(Guid.NewGuid(), Guid.NewGuid(), "Nombre de la Escuela");

        if (deleted)
        {
            escuela.Delete();
        }
        else
        {
            EscuelaRepository.GetByIdAsync(Arg.Is<Guid>(y => y == escuela.Id)).Returns(escuela);
        }

        return escuela;
    }
}
