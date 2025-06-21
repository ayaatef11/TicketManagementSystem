using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.ViewModels.Responses
{
    public class TicketIssueViewModelResponse
    {
        public List<IssueType> IssueTypes { get; set; }
        public TicketViewModelResponse ticketViewModelResponse { get; set; }
    }
}
