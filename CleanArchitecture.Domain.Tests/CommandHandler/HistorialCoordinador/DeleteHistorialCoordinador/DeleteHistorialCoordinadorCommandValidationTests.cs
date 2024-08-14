using System;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.DeleteHistorialCoordinador;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.DeleteHistorialCoordinador;

public sealed class DeleteHistorialCoordinadorCommandValidationTests :
    ValidationTestBase<DeleteHistorialCoordinadorCommand, DeleteHistorialCoordinadorCommandValidation>
{
    public DeleteHistorialCoordinadorCommandValidationTests() : base(new DeleteHistorialCoordinadorCommandValidation())
    {
    }

    [Fact] //C1
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact] //C2
    public void Should_Be_Invalid_For_HistorialCoordinador_Id()
    {
        var command = CreateTestCommand(id: Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.HistorialCoordinador.EmptyId,
            "HistorialCoordinador id may not be empty");
    }

    private static DeleteHistorialCoordinadorCommand CreateTestCommand(
    Guid? id = null)
    {
        return new DeleteHistorialCoordinadorCommand(
            id ?? Guid.NewGuid());
    }
}