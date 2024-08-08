using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Citas.UpdateCita;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Cita;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.UpdateCita;

public sealed class UpdateCitaCommandHandlerTests
{
    private readonly UpdateCitaCommandTestFixture _fixture = new();

}