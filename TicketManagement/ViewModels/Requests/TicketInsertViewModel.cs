using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TicketManagement.DAL.Enums;

namespace TicketManagement.ViewModels.Requests
{
    public class TicketInsertViewModel
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public List<SelectListItem> IssueTypes { get; set; } = new List<SelectListItem>();
    }
}
