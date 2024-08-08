using System;
using CleanArchitecture.Domain.Commands.Citas.DeleteCita;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.DeleteCita;

public sealed class DeleteCitaCommandValidationTests :
    ValidationTestBase<DeleteCitaCommand, DeleteCitaCommandValidation>
{
    public DeleteCitaCommandValidationTests() : base(new DeleteCitaCommandValidation())
    {
    }

}