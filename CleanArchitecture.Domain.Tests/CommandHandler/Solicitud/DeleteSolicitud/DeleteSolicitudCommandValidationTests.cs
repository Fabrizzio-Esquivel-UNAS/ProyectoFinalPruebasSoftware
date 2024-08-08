using System;
using CleanArchitecture.Domain.Commands.Solicitudes.DeleteSolicitud;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Solicitud.DeleteSolicitud;

public sealed class DeleteSolicitudCommandValidationTests :
    ValidationTestBase<DeleteSolicitudCommand, DeleteSolicitudCommandValidation>
{
    public DeleteSolicitudCommandValidationTests() : base(new DeleteSolicitudCommandValidation())
    {
    }

}