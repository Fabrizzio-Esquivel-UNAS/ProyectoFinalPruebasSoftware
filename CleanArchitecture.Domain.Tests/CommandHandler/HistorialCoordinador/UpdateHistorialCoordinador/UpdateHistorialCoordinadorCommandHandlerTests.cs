using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.HistorialCoordinador;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.UpdateHistorialCoordinador;

public sealed class UpdateHistorialCoordinadorCommandHandlerTests
{
    private readonly UpdateHistorialCoordinadorCommandTestFixture _fixture = new();

}