
using System;
using TicketManagement.DAL.Enums;
using TicketManagement.DAL.Models;
namespace TicketManagement.DAL.DTOS
{
    public record TicketDto
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; } 
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IssueName { get; set; } 
        public Status Status { get; set; }
        public IssueType IssueType { get; set; }

    }
}
