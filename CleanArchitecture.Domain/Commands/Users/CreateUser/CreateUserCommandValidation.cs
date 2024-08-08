using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Extensions.Validation;
using FluentValidation;
using System;

namespace CleanArchitecture.Domain.Commands.Users.CreateUser;

public sealed class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidation()
    {
        AddRuleForId();
        //AddRuleForTenantId();
        AddRuleForEmail();
        AddRuleForFirstName();
        AddRuleForLastName();
        AddRuleForPassword();
        AddRuleForTelefono();
        AddRuleForCodigo();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.UserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.User.EmptyId)
            .WithMessage("User id may not be empty");
    }

    //private void AddRuleForTenantId()
    //{
    //    RuleFor(cmd => cmd.TenantId)
    //        .NotEmpty()
    //        .WithErrorCode(DomainErrorCodes.Tenant.EmptyId)
    //        .WithMessage("Tenant id may not be empty");
    //}

    private void AddRuleForEmail()
    {
        RuleFor(cmd => cmd.Email)
            .EmailAddress()
            .WithErrorCode(DomainErrorCodes.User.InvalidEmail)
            .WithMessage("Email is not a valid email address")
            .MaximumLength(MaxLengths.User.Email)
            .WithErrorCode(DomainErrorCodes.User.EmailExceedsMaxLength)
            .WithMessage($"Email may not be longer than {MaxLengths.User.Email} characters");
    }

    private void AddRuleForFirstName()
    {
        RuleFor(cmd => cmd.FirstName)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.User.EmptyFirstName)
            .WithMessage("FirstName may not be empty")
            .MaximumLength(MaxLengths.User.FirstName)
            .WithErrorCode(DomainErrorCodes.User.FirstNameExceedsMaxLength)
            .WithMessage($"FirstName may not be longer than {MaxLengths.User.FirstName} characters");
    }

    private void AddRuleForLastName()
    {
        RuleFor(cmd => cmd.LastName)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.User.EmptyLastName)
            .WithMessage("LastName may not be empty")
            .MaximumLength(MaxLengths.User.LastName)
            .WithErrorCode(DomainErrorCodes.User.LastNameExceedsMaxLength)
            .WithMessage($"LastName may not be longer than {MaxLengths.User.LastName} characters");
    }

    private void AddRuleForPassword()
    {
        RuleFor(cmd => cmd.Password)
            .Password();
    }

    private void AddRuleForTelefono()
    {
        RuleFor(cmd => cmd.Telefono)
            .PhoneNumber()
            .WithErrorCode(DomainErrorCodes.User.InvalidTelefono)
            .WithMessage("Telefono is not a valid numero de telefono");
    }

    private void AddRuleForCodigo()
    {
        RuleFor(cmd => cmd.Codigo)
            .Codigo()
            .WithErrorCode(DomainErrorCodes.User.InvalidCodigo)
            .WithMessage("Codigo is not a valid codigo de estudiante");
    }
}