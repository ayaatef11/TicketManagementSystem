using FluentValidation;
using System;

namespace TicketManagement.DAL.Models.Validations
{
    public sealed class CustomerTicketValidations : AbstractValidator<CustomerTicket>
        {
            public CustomerTicketValidations()
            {
                RuleFor(x => x.FullName)
                        .NotEmpty()
                        .WithMessage("Name is required")
                        .MaximumLength(30)
                        .WithMessage("Name must not exceed 30 characters");
                RuleFor(x => x.MobileNumber)
                    .NotEmpty().WithMessage("Mobile Number is required");

                RuleFor(p => p.Email)
                   .NotEmpty().WithMessage("Email is required")
                   .EmailAddress().WithMessage("Email invalid");

                RuleFor(x => x.Description)
                    .NotEmpty()
                    .WithMessage("Description is required").Length(20, 250).WithMessage("invalid description length");

                RuleFor(p => p.CreatedDate)
                     .NotEmpty().WithMessage("Date is required")
                     .LessThan(p => DateTime.Now).WithMessage("Created Date should not be less than current date");

                RuleFor(x => x.IssueType)
                     .NotEmpty()
                     .WithMessage("issue type is required");
                RuleFor(x => x.Status)
                     .NotEmpty()
                     .WithMessage("Status is required");

                RuleFor(x => x.Priority)
                     .NotEmpty()
                     .WithMessage("Priority is required");

            }
        }
    }


