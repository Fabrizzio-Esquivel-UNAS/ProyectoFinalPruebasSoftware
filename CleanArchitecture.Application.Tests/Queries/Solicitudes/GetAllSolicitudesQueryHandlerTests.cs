using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Citas.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Citas;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Citas
{
    /// <summary>
    /// Pruebas unitarias para GetAllSolicitudesQueryHandler.
    /// </summary>
    public sealed class GetAllSolicitudesQueryHandlerTests
    {
        private readonly GetAllCitasTestFixture _fixture = new();

        /// <summary>
        /// Prueba que una cita existente se recupere correctamente.
        /// </summary>
        [Fact]
        public async Task Should_Get_Existing_Cita()
        {
            // Arrange
            var cita = _fixture.SetupCita();

            var query = new PageQuery
            {
                PageSize = 10,
                Page = 1
            };

            // Act
            var result = await _fixture.QueryHandler.Handle(
                new GetAllCitasQuery(query, false),
                default);

            // Assert
            _fixture.VerifyNoDomainNotification();

            result.PageSize.Should().Be(query.PageSize);
            result.Page.Should().Be(query.Page);
            result.Count.Should().Be(1);

            cita.Should().BeEquivalentTo(result.Items.First());
        }

        /// <summary>
        /// Prueba que una cita eliminada no se recupere.
        /// </summary>
        [Fact]
        public async Task Should_Not_Get_Deleted_Cita()
        {
            // Arrange
            _fixture.SetupCita(true);

            var query = new PageQuery
            {
                PageSize = 10,
                Page = 1
            };

            // Act
            var result = await _fixture.QueryHandler.Handle(
                new GetAllCitasQuery(query, false),
                default);

            // Assert
            result.PageSize.Should().Be(query.PageSize);
            result.Page.Should().Be(query.Page);
            result.Count.Should().Be(0);

            result.Items.Should().HaveCount(0);
        }
    }
}
