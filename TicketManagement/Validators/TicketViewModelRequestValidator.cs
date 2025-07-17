using FluentValidation;
using TicketManagement.ViewModels.Requests;
using TicketManagement.ViewModels.Responses;

namespace TicketManagement.Validators
{
    public class TicketViewModelRequestValidator : AbstractValidator<TicketViewModelRequest>
    {
        public TicketViewModelRequestValidator()
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
                .WithMessage("Description is required");


            RuleFor(x => x.IssueName)
                .NotEmpty()
                .WithMessage("IssueName is required");

            RuleFor(x => x.Status)
              .IsInEnum()
              .WithMessage("Status is required");

            RuleFor(x => x.Priority)
              .IsInEnum()
              .WithMessage("Priority is required");
        }
    }
}