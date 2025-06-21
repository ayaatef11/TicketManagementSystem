using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Enums;
using TicketManagement.DAL.Models;

namespace TicketManagement.ViewModels.Requests
{
    public record TicketFilterViewModelRequest
    {
        public int? IssueTypeId { get; set; }
        public Priority? Priority { get; set; }
    }
}
