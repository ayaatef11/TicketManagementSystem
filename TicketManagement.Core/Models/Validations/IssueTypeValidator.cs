using FluentValidation;
using TicketManagement.DAL.Models;

namespace TicketManagement.DAL.Models.Validations
{
    public sealed  class IssueTypeValidator : AbstractValidator<IssueType>
    {
        public IssueTypeValidator()
        {
            RuleFor(x => x.IssueTypeName)
              .NotEmpty()
              .WithMessage("Issue Name is required");
        }
    }
}