using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.CreateCita;

public sealed class CreateOrUpdateCitaCommandValidationTests :
    ValidationTestBase<CreateOrUpdateCitaCommand, CreateOrUpdateCitaCommandValidation>
{
    public CreateOrUpdateCitaCommandValidationTests() : base(new CreateOrUpdateCitaCommandValidation())
    {
    }

}