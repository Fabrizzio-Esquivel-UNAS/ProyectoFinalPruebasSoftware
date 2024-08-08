using System;
using CleanArchitecture.Application.Queries.Solicitudes.GetSolicitudById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Solicitudes
{
    /// <summary>
    /// Fixture para configurar las pruebas del manejador GetSolicitudByIdQueryHandler.
    /// </summary>
    public sealed class GetSolicitudByIdTestFixture : QueryHandlerBaseFixture
    {
        public GetSolicitudByIdQueryHandler QueryHandler { get; }
        private ISolicitudRepository SolicitudRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase GetSolicitudByIdTestFixture.
        /// Configura el QueryHandler y el mock del SolicitudRepository.
        /// </summary>
        public GetSolicitudByIdTestFixture()
        {
            SolicitudRepository = Substitute.For<ISolicitudRepository>();

            QueryHandler = new GetSolicitudByIdQueryHandler(
                SolicitudRepository,
                Bus);
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
                "");

            if (deleted)
            {
                solicitud.Delete();
            }
            else
            {
                SolicitudRepository.GetByIdAsync(Arg.Is<Guid>(y => y == solicitud.Id)).Returns(solicitud);
            }

            return solicitud;
        }
    }
}
