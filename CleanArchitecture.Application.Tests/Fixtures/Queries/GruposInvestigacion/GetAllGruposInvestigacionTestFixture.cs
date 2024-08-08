using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.GruposInvestigacion.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.GruposInvestigacion;

public sealed class GetAllGruposInvestigacionTestFixture : QueryHandlerBaseFixture
{
    public GetAllGruposInvestigacionQueryHandler QueryHandler { get; }
    private IGrupoInvestigacionRepository GrupoInvestigacionRepository { get; }
    private IHistorialCoordinadorRepository HistorialCoordinadorRepository { get; }

    public GetAllGruposInvestigacionTestFixture()
    {
        GrupoInvestigacionRepository = Substitute.For<IGrupoInvestigacionRepository>();
        HistorialCoordinadorRepository = Substitute.For<IHistorialCoordinadorRepository>();
        var sortingProvider = new GrupoInvestigacionViewModelSortProvider();

        QueryHandler = new GetAllGruposInvestigacionQueryHandler(GrupoInvestigacionRepository, HistorialCoordinadorRepository, sortingProvider);
    }

    public GrupoInvestigacion SetupGrupoInvestigacion(bool deleted = false)
    {
        var grupoInvestigacion = new GrupoInvestigacion(Guid.NewGuid(), "Nombre del Grupo de Investigación");

        if (deleted)
        {
            grupoInvestigacion.Delete();
        }

        var grupoInvestigacionList = new List<GrupoInvestigacion> { grupoInvestigacion }.BuildMock();
        GrupoInvestigacionRepository.GetAllNoTracking().Returns(grupoInvestigacionList);

        return grupoInvestigacion;
    }
}
