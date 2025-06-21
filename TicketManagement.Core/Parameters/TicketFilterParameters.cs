using TicketManagement.DAL.Enums;
using TicketManagement.DAL.Models;

namespace TicketManagement.DAL.Parameters
{
    public record TicketFilterParameters
    {
        public int? IssueTypeId { get; set;}
        public Priority? Priority { get; set; }
    }
}
