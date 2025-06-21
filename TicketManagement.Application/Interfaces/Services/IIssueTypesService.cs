using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces.Services
{
    public interface IIssueTypesService
    {
        Task<List<IssueType>> GetAllAsync();
    }
}
