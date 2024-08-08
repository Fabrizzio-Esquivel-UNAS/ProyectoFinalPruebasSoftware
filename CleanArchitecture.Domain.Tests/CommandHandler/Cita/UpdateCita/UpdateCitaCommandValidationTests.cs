using System;
using CleanArchitecture.Domain.Commands.Citas.UpdateCita;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.UpdateCita;

public sealed class UpdateCitaCommandValidationTests :
    ValidationTestBase<UpdateCitaCommand, UpdateCitaCommandValidation>
{
    public UpdateCitaCommandValidationTests() : base(new UpdateCitaCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Cita_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Cita.EmptyId,
            "Cita id may not be empty");
    }

    private static UpdateCitaCommand CreateTestCommand(
        Guid? citaId = null,
        CitaEstado? estado = null,
        string? desarrolloAsesor = null,
        string? desarrolloAsesorado = null)
    {
        return new UpdateCitaCommand(
            citaId ?? Guid.NewGuid(),
            estado ?? CitaEstado.Justificado,
            desarrolloAsesor ?? "",
            desarrolloAsesorado ?? "");
    }
}