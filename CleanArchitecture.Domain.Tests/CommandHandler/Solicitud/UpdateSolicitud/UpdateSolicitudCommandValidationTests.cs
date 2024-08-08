using System;
using CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.UpdateSolicitud;

public sealed class UpdateSolicitudCommandValidationTests :
    ValidationTestBase<UpdateSolicitudCommand, UpdateSolicitudCommandValidation>
{
    public UpdateSolicitudCommandValidationTests() : base(new UpdateSolicitudCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Solicitud.EmptyId,
            "Solicitud id may not be empty");
    }

    private static UpdateSolicitudCommand CreateTestCommand(
        Guid? id = null,
        SolicitudStatus? estado = null)
    {
        return new UpdateSolicitudCommand(
            id ?? Guid.NewGuid(),
            estado ?? SolicitudStatus.Rechazado);
    }
}