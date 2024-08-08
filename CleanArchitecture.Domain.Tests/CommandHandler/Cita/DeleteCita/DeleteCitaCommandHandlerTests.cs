using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Citas.DeleteCita;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Cita;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.DeleteCita;

public sealed class DeleteCitaCommandHandlerTests
{
    private readonly DeleteCitaCommandTestFixture _fixture = new();

}