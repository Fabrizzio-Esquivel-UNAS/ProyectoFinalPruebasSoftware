using System;
using System.Collections.Generic;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.UpdateHistorialCoordinador;

public sealed class UpdateHistorialCoordinadorCommandValidationTests :
    ValidationTestBase<UpdateHistorialCoordinadorCommand, UpdateHistorialCoordinadorCommandValidation>
{
    public UpdateHistorialCoordinadorCommandValidationTests() : base(new UpdateHistorialCoordinadorCommandValidation())
    {
    }

    [Fact] // C1
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact] // C2
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id()
    {
        var command = CreateTestCommand(id: Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.HistorialCoordinador.EmptyId,
            "HistorialCoordinador id may not be empty");
    }

    [Fact] // C3
    public void Should_Be_Invalid_For_Invalid_FechaFin()
    {
        var command = CreateTestCommand(fechaFin: default(DateTime));

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.HistorialCoordinador.InvalidFechaFin,
            "FechaFin is not a valid Fecha");
    }

    [Fact] // C4
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id_And_Invalid_FechaFin()
    {
        var command = CreateTestCommand(id: Guid.Empty, fechaFin: default(DateTime));

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyId,
        DomainErrorCodes.HistorialCoordinador.InvalidFechaFin,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    private static UpdateHistorialCoordinadorCommand CreateTestCommand(
        Guid? id = null,
        DateTime? fechaFin = null)
    {
        return new UpdateHistorialCoordinadorCommand(
            id ?? Guid.NewGuid(),
            fechaFin ?? DateTime.UtcNow);
    }
}