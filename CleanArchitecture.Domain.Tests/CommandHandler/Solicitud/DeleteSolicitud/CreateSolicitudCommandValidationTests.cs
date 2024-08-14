using System;
using System.Collections.Generic;
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
        var command = CreateTestCommand(id: Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Solicitud.EmptyId,
            "Solicitud id may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesoradoUserId()
    {
        var command = CreateTestCommand(asesoradoUserId: Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.User.EmptyId,
            "Asesorado user id may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesorUserId()
    {
        var command = CreateTestCommand(asesorUserId: Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.User.EmptyId,
            "Asesor user id may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_Zero_NumeroTesis()
    {
        var command = CreateTestCommand(numeroTesis: 0);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Solicitud.InvalidNumeroTesis,
            "Numero tesis must be greater than zero");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Mensaje()
    {
        var command = CreateTestCommand(mensaje: string.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Solicitud.EmptyMensaje,
            "Mensaje may not be empty");
    }

    // Add more test cases for combinations

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesoradoUserId()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesoradoUserId: Guid.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
                new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesorUserId()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesorUserId: Guid.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
                new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Zero_NumeroTesis()
    {
        var command = CreateTestCommand(id: Guid.Empty, numeroTesis: 0);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
                new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(id: Guid.Empty, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
                new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesoradoUserId_And_Empty_AsesorUserId()
    {
        var command = CreateTestCommand(asesoradoUserId: Guid.Empty, asesorUserId: Guid.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
                new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesoradoUserId_And_Zero_NumeroTesis()
    {
        var command = CreateTestCommand(asesoradoUserId: Guid.Empty, numeroTesis: 0);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
                new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesoradoUserId_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(asesoradoUserId: Guid.Empty, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
                new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesorUserId_And_Zero_NumeroTesis()
    {
        var command = CreateTestCommand(asesorUserId: Guid.Empty, numeroTesis: 0);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
                new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesorUserId_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(asesorUserId: Guid.Empty, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
                new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Zero_NumeroTesis_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(numeroTesis: 0, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
                new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero"),
                new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesoradoUserId_And_Empty_AsesorUserId()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesoradoUserId: Guid.Empty, asesorUserId: Guid.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesoradoUserId_And_Zero_NumeroTesis()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesoradoUserId: Guid.Empty, numeroTesis: 0);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesoradoUserId_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesoradoUserId: Guid.Empty, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesorUserId_And_Zero_NumeroTesis()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesorUserId: Guid.Empty, numeroTesis: 0);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesorUserId_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesorUserId: Guid.Empty, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Zero_NumeroTesis_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(id: Guid.Empty, numeroTesis: 0, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesoradoUserId_And_Empty_AsesorUserId_And_Zero_NumeroTesis()
    {
        var command = CreateTestCommand(asesoradoUserId: Guid.Empty, asesorUserId: Guid.Empty, numeroTesis: 0);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesoradoUserId_And_Empty_AsesorUserId_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(asesoradoUserId: Guid.Empty, asesorUserId: Guid.Empty, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesoradoUserId_And_Zero_NumeroTesis_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(asesoradoUserId: Guid.Empty, numeroTesis: 0, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesorUserId_And_Zero_NumeroTesis_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(asesorUserId: Guid.Empty, numeroTesis: 0, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesoradoUserId_And_Empty_AsesorUserId_And_Zero_NumeroTesis()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesoradoUserId: Guid.Empty, asesorUserId: Guid.Empty, numeroTesis: 0);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesoradoUserId_And_Empty_AsesorUserId_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesoradoUserId: Guid.Empty, asesorUserId: Guid.Empty, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AsesoradoUserId_And_Empty_AsesorUserId_And_Zero_NumeroTesis_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(asesoradoUserId: Guid.Empty, asesorUserId: Guid.Empty, numeroTesis: 0, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Solicitud_Id_And_Empty_AsesoradoUserId_And_Empty_AsesorUserId_And_Zero_NumeroTesis_And_Empty_Mensaje()
    {
        var command = CreateTestCommand(id: Guid.Empty, asesoradoUserId: Guid.Empty, asesorUserId: Guid.Empty, numeroTesis: 0, mensaje: string.Empty);

        ShouldHaveExpectedErrors(
            command,
            new KeyValuePair<string, string>[]
            {
            new(DomainErrorCodes.Solicitud.EmptyId, "Solicitud id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesorado user id may not be empty"),
            new(DomainErrorCodes.User.EmptyId, "Asesor user id may not be empty"),
            new(DomainErrorCodes.Solicitud.InvalidNumeroTesis, "Numero tesis must be greater than zero"),
            new(DomainErrorCodes.Solicitud.EmptyMensaje, "Mensaje may not be empty")
            });
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
