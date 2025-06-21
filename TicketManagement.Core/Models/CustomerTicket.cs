using System;
using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Enums;
using TicketManagement.DAL.Models.Common;

namespace TicketManagement.DAL.Models
{
    public class CustomerTicket:BaseEntity
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IssueTypeID { get; set; }
        public IssueType IssueType { get; set; }
        public Status Status { get; set; }  
    }
}
