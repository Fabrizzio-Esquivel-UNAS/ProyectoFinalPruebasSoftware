using System;
using CleanArchitecture.Domain.Commands.Tenants.DeleteTenant;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Tenant.DeleteTenant;

public sealed class DeleteTenantCommandValidationTests :
    ValidationTestBase<DeleteTenantCommand, DeleteTenantCommandValidation>
{
    public DeleteTenantCommandValidationTests() : base(new DeleteTenantCommandValidation())
    {
    }

}