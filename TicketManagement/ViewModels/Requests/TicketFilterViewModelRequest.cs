using TicketManagement.DAL.Enums;

namespace TicketManagement.ViewModels.Requests
{
    public record TicketFilterViewModelRequest
    {
        public int? IssueTypeId { get; set; }
        public Priority? Priority { get; set; }
    }
}
