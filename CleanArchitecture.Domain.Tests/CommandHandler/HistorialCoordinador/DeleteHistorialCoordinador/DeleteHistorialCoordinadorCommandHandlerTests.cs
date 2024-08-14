using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.DeleteHistorialCoordinador;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.HistorialCoordinador;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.DeleteHistorialCoordinador;

public sealed class DeleteHistorialCoordinadorCommandHandlerTests
{
    private readonly DeleteHistorialCoordinadorCommandTestFixture _fixture = new();
}