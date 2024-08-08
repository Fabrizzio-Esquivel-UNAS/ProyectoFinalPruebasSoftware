using System;
using CleanArchitecture.Application.Queries.GruposInvestigacion.GetGrupoInvestigacionById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.GruposInvestigacion;

public sealed class GetGrupoInvestigacionByIdTestFixture : QueryHandlerBaseFixture
{
    public GetGrupoInvestigacionByIdQueryHandler QueryHandler { get; }
    private IGrupoInvestigacionRepository GrupoInvestigacionRepository { get; }
    private IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public GetGrupoInvestigacionByIdTestFixture()
    {
        GrupoInvestigacionRepository = Substitute.For<IGrupoInvestigacionRepository>();
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();

        QueryHandler = new GetGrupoInvestigacionByIdQueryHandler(
            GrupoInvestigacionRepository,
            HistorialCoordinadorRepository,
            Bus);
    }

    public GrupoInvestigacion SetupGrupoInvestigacion(bool deleted = false)
    {
        var grupoInvestigacion = new GrupoInvestigacion(Guid.NewGuid(), "Nombre del Grupo de Investigación");

        if (deleted)
        {
            grupoInvestigacion.Delete();
        }
        else
        {
            GrupoInvestigacionRepository.GetByIdAsync(Arg.Is<Guid>(y => y == grupoInvestigacion.Id)).Returns(grupoInvestigacion);
        }

        return grupoInvestigacion;
    }
}
