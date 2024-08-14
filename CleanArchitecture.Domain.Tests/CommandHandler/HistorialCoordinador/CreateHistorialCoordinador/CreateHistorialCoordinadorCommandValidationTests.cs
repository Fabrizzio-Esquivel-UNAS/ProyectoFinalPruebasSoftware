using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.HistorialCoordinador.CreateHistorialCoordinador;

public sealed class CreateHistorialCoordinadorCommandValidationTests :
    ValidationTestBase<CreateHistorialCoordinadorCommand, CreateHistorialCoordinadorCommandValidation>
{
    public CreateHistorialCoordinadorCommandValidationTests() : base(new CreateHistorialCoordinadorCommandValidation())
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
        var command = CreateTestCommand(historialcoordinadorId: Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.HistorialCoordinador.EmptyId,
            "HistorialCoordinador id may not be empty");
    }

    [Fact] // C3
    public void Should_Be_Invalid_For_Empty_User_Id()
    {
        var command = CreateTestCommand(userId: Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.HistorialCoordinador.EmptyUserId,
            "User id may not be empty");
    }

    [Fact] // C4
    public void Should_Be_Invalid_For_Empty_GrupoInvestigacion_Id()
    {
        var command = CreateTestCommand(grupoInvestigacionId: Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId,
            "GrupoInvestigacion id may not be empty");
    }

    [Fact] // C5
    public void Should_Be_Invalid_For_Invalid_FechaInicio()
    {
        var command = CreateTestCommand(fechaInicio: default(DateTime));

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio,
            "FechaInicio is not a valid Fecha");
    }

    [Fact] // C6
    public void Should_Be_Invalid_For_Empty_User_Id_And_Empty_GrupoInvestigacion_Id()
    {
        var command = CreateTestCommand(userId: Guid.Empty, grupoInvestigacionId: Guid.Empty);

        var errors = new List<string>
        {
            DomainErrorCodes.HistorialCoordinador.EmptyUserId,
            DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId,
        };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C7
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id_And_Empty_User_Id()
    {
        var command = CreateTestCommand(historialcoordinadorId: Guid.Empty, userId: Guid.Empty);

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyId,
        DomainErrorCodes.HistorialCoordinador.EmptyUserId,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C8
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id_And_Empty_GrupoInvestigacion_Id()
    {
        var command = CreateTestCommand(historialcoordinadorId: Guid.Empty, grupoInvestigacionId: Guid.Empty);

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyId,
        DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C9
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id_And_Invalid_FechaInicio()
    {
        var command = CreateTestCommand(historialcoordinadorId: Guid.Empty, fechaInicio: default(DateTime));

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyId,
        DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C10
    public void Should_Be_Invalid_For_Empty_User_Id_And_Invalid_FechaInicio()
    {
        var command = CreateTestCommand(userId: Guid.Empty, fechaInicio: default(DateTime));

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyUserId,
        DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C11
    public void Should_Be_Invalid_For_Empty_GrupoInvestigacion_Id_And_Invalid_FechaInicio()
    {
        var command = CreateTestCommand(grupoInvestigacionId: Guid.Empty, fechaInicio: default(DateTime));

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId,
        DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C12
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id_And_Empty_User_Id_And_Empty_GrupoInvestigacion_Id()
    {
        var command = CreateTestCommand(historialcoordinadorId: Guid.Empty, userId: Guid.Empty, grupoInvestigacionId: Guid.Empty);

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyId,
        DomainErrorCodes.HistorialCoordinador.EmptyUserId,
        DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C13
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id_And_Empty_User_Id_And_Invalid_FechaInicio()
    {
        var command = CreateTestCommand(historialcoordinadorId: Guid.Empty, userId: Guid.Empty, fechaInicio: default(DateTime));

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyId,
        DomainErrorCodes.HistorialCoordinador.EmptyUserId,
        DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C14
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id_And_Empty_GrupoInvestigacion_Id_And_Invalid_FechaInicio()
    {
        var command = CreateTestCommand(historialcoordinadorId: Guid.Empty, grupoInvestigacionId: Guid.Empty, fechaInicio: default(DateTime));

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyId,
        DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId,
        DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C15
    public void Should_Be_Invalid_For_Empty_User_Id_And_Empty_GrupoInvestigacion_Id_And_Invalid_FechaInicio()
    {
        var command = CreateTestCommand(userId: Guid.Empty, grupoInvestigacionId: Guid.Empty, fechaInicio: default(DateTime));

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyUserId,
        DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId,
        DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact] // C16
    public void Should_Be_Invalid_For_Empty_HistorialCoordinador_Id_And_Empty_User_Id_And_Empty_GrupoInvestigacion_Id_And_Invalid_FechaInicio()
    {
        var command = CreateTestCommand(historialcoordinadorId: Guid.Empty, userId: Guid.Empty, grupoInvestigacionId: Guid.Empty, fechaInicio: default(DateTime));

        var errors = new List<string>
    {
        DomainErrorCodes.HistorialCoordinador.EmptyId,
        DomainErrorCodes.HistorialCoordinador.EmptyUserId,
        DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId,
        DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio,
    };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    private static CreateHistorialCoordinadorCommand CreateTestCommand(
        Guid? historialcoordinadorId = null,
        Guid? userId = null,
        Guid? grupoInvestigacionId = null,
        DateTime? fechaInicio = null)
    {
        return new CreateHistorialCoordinadorCommand(
            historialcoordinadorId ?? Guid.NewGuid(),
            userId ?? Guid.NewGuid(),
            grupoInvestigacionId ?? Guid.NewGuid(),
            fechaInicio ?? DateTime.UtcNow);
    }
}