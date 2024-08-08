using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Solicitudes.GetSolicitudById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Solicitudes;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Solicitudes
{
    /// <summary>
    /// Pruebas unitarias para GetSolicitudByIdQueryHandler.
    /// </summary>
    public sealed class GetSolicitudByIdQueryHandlerTests
    {
        private readonly GetSolicitudByIdTestFixture _fixture = new();

        /// <summary>
        /// Prueba que una solicitud existente se recupere correctamente.
        /// </summary>
        [Fact]
        public async Task Should_Get_Existing_Solicitud()
        {
            // Arrange
            var solicitud = _fixture.SetupSolicitud();

            // Act
            var result = await _fixture.QueryHandler.Handle(
                new GetSolicitudByIdQuery(solicitud.Id),
                default);

            // Assert
            _fixture.VerifyNoDomainNotification();

            solicitud.Should().BeEquivalentTo(result);
        }

        /// <summary>
        /// Prueba que una solicitud eliminada no se recupere.
        /// </summary>
        [Fact]
        public async Task Should_Not_Get_Deleted_Solicitud()
        {
            // Arrange
            var solicitud = _fixture.SetupSolicitud(true);

            // Act
            var result = await _fixture.QueryHandler.Handle(
                new GetSolicitudByIdQuery(solicitud.Id),
                default);

            // Assert
            _fixture.VerifyExistingNotification(
                nameof(GetSolicitudByIdQuery),
                ErrorCodes.ObjectNotFound,
                $"Solicitud with id {solicitud.Id} could not be found");
            result.Should().BeNull();
        }
    }
}
