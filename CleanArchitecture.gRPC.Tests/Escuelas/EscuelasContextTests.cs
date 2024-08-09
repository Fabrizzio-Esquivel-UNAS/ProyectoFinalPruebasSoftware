using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Contexts;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.Escuelas;
using CleanArchitecture.Shared.Escuela;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Contexts
{
    public class EscuelasContextTests
    {
        private readonly EscuelasApi.EscuelasApiClient _clientMock;
        private readonly EscuelasContext _escuelasContext;

        public EscuelasContextTests()
        {
            _clientMock = Substitute.For<EscuelasApi.EscuelasApiClient>();
            _escuelasContext = new EscuelasContext(_clientMock);
        }

        [Fact]
        public async Task GetEscuelasByIds_Should_Return_Empty_List_If_No_Ids_Provided()
        {
            // Arrange
            var ids = Enumerable.Empty<Guid>();

            _clientMock.GetByIdsAsync(Arg.Any<GetEscuelasByIdsRequest>())
                .Returns(new GetEscuelasByIdsResponse());

            // Act
            var result = await _escuelasContext.GetEscuelasByIds(ids);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetEscuelasByIds_Should_Return_Correct_EscuelaViewModel_For_Single_Id()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedEscuela = new EscuelaViewModel(id, "Test Escuela");

            var response = new GetEscuelasByIdsResponse();
            response.Escuelas.Add(new EscuelaProto
            {
                Id = id.ToString(),
                Nombre = "Test Escuela"
            });

            _clientMock.GetByIdsAsync(Arg.Is<GetEscuelasByIdsRequest>(req =>
                req.Ids.Contains(id.ToString())))
                .Returns(response);

            // Act
            var result = await _escuelasContext.GetEscuelasByIds(new[] { id });

            // Assert
            result.Should().ContainSingle()
                .Which.Should().BeEquivalentTo(expectedEscuela);
        }

        [Fact]
        public async Task GetEscuelasByIds_Should_Handle_Multiple_Ids_Correctly()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var response = new GetEscuelasByIdsResponse();
            response.Escuelas.Add(new EscuelaProto
            {
                Id = id1.ToString(),
                Nombre = "Escuela 1"
            });
            response.Escuelas.Add(new EscuelaProto
            {
                Id = id2.ToString(),
                Nombre = "Escuela 2"
            });

            _clientMock.GetByIdsAsync(Arg.Is<GetEscuelasByIdsRequest>(req =>
                req.Ids.Contains(id1.ToString()) && req.Ids.Contains(id2.ToString())))
                .Returns(response);

            // Act
            var result = await _escuelasContext.GetEscuelasByIds(new[] { id1, id2 });

            // Assert
            result.Should().HaveCount(2);
            result.Should().ContainEquivalentOf(new EscuelaViewModel(id1, "Escuela 1"));
            result.Should().ContainEquivalentOf(new EscuelaViewModel(id2, "Escuela 2"));
        }

        [Fact]
        public async Task GetEscuelasByIds_Should_Ignore_Nonexistent_Ids()
        {
            // Arrange
            var existentId = Guid.NewGuid();
            var nonexistentId = Guid.NewGuid();

            var response = new GetEscuelasByIdsResponse();
            response.Escuelas.Add(new EscuelaProto
            {
                Id = existentId.ToString(),
                Nombre = "Existing Escuela"
            });

            _clientMock.GetByIdsAsync(Arg.Is<GetEscuelasByIdsRequest>(req =>
                req.Ids.Contains(existentId.ToString()) && req.Ids.Contains(nonexistentId.ToString())))
                .Returns(response);

            // Act
            var result = await _escuelasContext.GetEscuelasByIds(new[] { existentId, nonexistentId });

            // Assert
            result.Should().HaveCount(1);
            result.First().Id.Should().Be(existentId);
        }

        [Fact]
        public async Task GetEscuelasByIds_Should_Throw_Exception_On_Client_Error()
        {
            // Arrange
            var ids = new[] { Guid.NewGuid() };

            _clientMock.GetByIdsAsync(Arg.Any<GetEscuelasByIdsRequest>())
                .Throws(new Exception("gRPC error"));

            // Act
            Func<Task> act = async () => await _escuelasContext.GetEscuelasByIds(ids);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("gRPC error");
        }
    }
}
