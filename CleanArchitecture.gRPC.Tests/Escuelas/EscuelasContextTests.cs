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

namespace CleanArchitecture.gRPC.Tests.Escuelas
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
