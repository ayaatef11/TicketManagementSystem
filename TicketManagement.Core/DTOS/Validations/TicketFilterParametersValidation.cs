using FluentValidation; 
using TicketManagement.DAL.Parameters;

namespace TicketManagement.DAL.DTOS.Validations
{
    public sealed class TicketFilterParametersValidation  : AbstractValidator<TicketFilterParameters>
    {
        public TicketFilterParametersValidation()
        {

            RuleFor(x => x.Priority)
              .NotEmpty()
              .WithMessage("Priority is required");

        }
    }
}
