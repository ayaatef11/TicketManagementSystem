using FluentValidation;
using TicketManagement.ViewModels.Requests;

namespace TicketManagement.Validators
{
    public class TicketFilterViewModelRequestValidator:AbstractValidator<TicketFilterViewModelRequest>
    {
        public TicketFilterViewModelRequestValidator()
        {
            RuleFor(x => x.IssueTypeId)
              .NotEmpty()
              .WithMessage("IssueTypeId is required");

            RuleFor(x => x.Priority)
              .NotEmpty()
              .WithMessage("Priority is required");
        }
    }
}
