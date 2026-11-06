using Application.DTO.Auth;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators.Auth
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidator()
        {
            this.RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FirstName).NotEmpty()
                                     .WithMessage("First name is required")
                                     .MinimumLength(3)
                                     .WithMessage("First name must contain at least 3 characters")
                                     .MaximumLength(30)
                                     .WithMessage("First name cannot contain more than 30 characters")
                                     .Matches("^[A-Z][a-z]{2,29}$")
                                     .WithMessage("First name must start with capital letter,have min 3 and max 30 letters");

            RuleFor(x => x.LastName).NotEmpty()
                                     .WithMessage("Last name is required")
                                     .MinimumLength(4)
                                     .WithMessage("Last name must contain at least 4 characters")
                                     .MaximumLength(50)
                                     .WithMessage("Last name cannot contain more than 50 characters")
                                     .Matches("^[A-Z][a-z]{3,49}$")
                                     .WithMessage("Last name must start with capital letter,have min 4 and max 50 letters");

            RuleFor(x => x.Email).NotEmpty()
                                     .WithMessage("Email is required")
                                     .EmailAddress()
                                     .WithMessage("Email address is invalid")
                                     .MaximumLength(300)
                                     .WithMessage("Email cannot contain more than 300 characters");

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Password is required")
                                    .MinimumLength(8)
                                    .WithMessage("Password must contain at least 8 characters")
                                    .MaximumLength(100)
                                    .WithMessage("Password cannot contain more than 100 characters")
                                    .Matches("[A-Z]")
                                    .WithMessage("Password must contain at least one uppercase letter")
                                    .Matches("[a-z]")
                                    .WithMessage("Password must contain at least one lowercase letter")
                                    .Matches("[0-9]")
                                    .WithMessage("Password must contain at least one digit");
        }
    }
}
