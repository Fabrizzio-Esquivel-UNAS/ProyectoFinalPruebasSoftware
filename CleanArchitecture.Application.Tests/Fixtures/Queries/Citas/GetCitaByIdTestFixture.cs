using System;
using CleanArchitecture.Application.Queries.Citas.GetCitaById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Citas;

public sealed class GetCitaByIdTestFixture : QueryHandlerBaseFixture
{
    public GetCitaByIdQueryHandler QueryHandler { get; }
    private ICitaRepository CitaRepository { get; }

    public GetCitaByIdTestFixture()
    {
        CitaRepository = Substitute.For<ICitaRepository>();

        QueryHandler = new GetCitaByIdQueryHandler(
            CitaRepository,
            Bus);
    }

    public Cita SetupCita(bool deleted = false)
    {
        var cita = new Cita(Guid.NewGuid(), "123", Guid.NewGuid(), Guid.NewGuid(), default, default, default);

        if (deleted)
        {
            cita.Delete();
        }
        else
        {
            CitaRepository.GetByIdAsync(Arg.Is<Guid>(y => y == cita.Id)).Returns(cita);
        }


        return cita;
    }
}