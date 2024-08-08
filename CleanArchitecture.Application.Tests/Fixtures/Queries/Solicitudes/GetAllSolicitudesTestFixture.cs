using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.Solicitudes.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Solicitudes
{
    /// <summary>
    /// Fixture para configurar las pruebas del manejador GetAllSolicitudesQueryHandler.
    /// </summary>
    public sealed class GetAllSolicitudesTestFixture : QueryHandlerBaseFixture
    {
        public GetAllSolicitudesQueryHandler QueryHandler { get; }
        private ISolicitudRepository SolicitudRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase GetAllSolicitudesTestFixture.
        /// Configura el QueryHandler y el mock del SolicitudRepository.
        /// </summary>
        public GetAllSolicitudesTestFixture()
        {
            SolicitudRepository = Substitute.For<ISolicitudRepository>();
            var sortingProvider = new SolicitudViewModelSortProvider();

            QueryHandler = new GetAllSolicitudesQueryHandler(SolicitudRepository, sortingProvider);
        }

        /// <summary>
        /// Configura una entidad Solicitud para fines de prueba.
        /// </summary>
        /// <param name="deleted">Indica si la Solicitud está marcada como eliminada.</param>
        /// <returns>La entidad Solicitud configurada.</returns>
        public Solicitud SetupSolicitud(bool deleted = false)
        {
            var solicitud = new Solicitud(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                1,
                "",
                SolicitudStatus.Pendiente
            );

            if (deleted)
            {
                solicitud.Delete();
            }

            var solicitudList = new List<Solicitud> { solicitud }.BuildMock();
            SolicitudRepository.GetAllNoTracking().Returns(solicitudList);

            return solicitud;
        }
    }
}
