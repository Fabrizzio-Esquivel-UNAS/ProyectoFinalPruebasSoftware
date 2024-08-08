using System;
using CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.CreateSolicitud;

public sealed class CreateSolicitudCommandValidationTests :
    ValidationTestBase<CreateSolicitudCommand, CreateSolicitudCommandValidation>
{
    public CreateSolicitudCommandValidationTests() : base(new CreateSolicitudCommandValidation())
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

    private static CreateSolicitudCommand CreateTestCommand(
        Guid? id = null,
        Guid? asesoradoUserId = null, 
        Guid? asesorUserId = null,
        int? numeroTesis = null, 
        string? mensaje = null)
    {
        return new CreateSolicitudCommand(
            id ?? Guid.NewGuid(),
            asesoradoUserId ?? Guid.NewGuid(),
            asesorUserId ?? Guid.NewGuid(),
            numeroTesis ?? 1,
            mensaje ?? "Test");
    }
}