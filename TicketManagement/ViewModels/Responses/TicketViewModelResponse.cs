using System;
using System.Collections.Generic;
using TicketManagement.DAL.Enums;
using TicketManagement.DAL.Models;

namespace TicketManagement.ViewModels.Responses
{
    public record TicketViewModelResponse
    {
        public int Id { get; set; }
        public Priority Priority { get; set; }
        public Status Status { set; get; } 
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; } 
        public string Description { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public string IssueName { get; set; } 
        public IssueType IssueType { get; set; }
    }
}
