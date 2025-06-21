using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.BLL.Results;
using TicketManagement.DAL.DTOS;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Parameters;

namespace TicketManagement.BLL.Interfaces.Services
{
    public interface ITicketsService
    {
        Task<Result<bool>> SaveTicket(TicketDto ticket);
        Task<Result<List<TicketDto>>> GetTicketList();

        Task<Result<List<TicketDto>>> FilterByIssueAndProperty(TicketFilterParameters parameters);

        Result<TicketDto> GetTicketById(int id);

        Task<Result<bool>> EditTicket(TicketDto ticket);
        Result<TicketDto> SearchForTicket(string name);
    }
}
