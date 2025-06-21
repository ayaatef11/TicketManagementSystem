using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using TicketManagement.BLL.Results;
using TicketManagement.DAL.DTOS;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Parameters;
using System.Threading.Tasks;
using AutoMapper;
using TicketManagement.BLL.Interfaces.Services;
using TicketManagement.BLL.Interfaces.UnitOfWork;
using TicketManagement.DAL.Enums;

namespace TicketManagement.BLL.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;
        public TicketsService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<bool>> SaveTicket(TicketDto ticket)
        {
            if (ticket == null)
            {
                return Result.Failure<bool>(new Error
                {
                    Title = "Invalid ticket",
                    StatusCode = StatusCodes.Status400BadRequest,
                });
            }

            try
            {
                var parameters = new SqlParameter[]
                   {
                new("@fullName", ticket.FullName),
                new("@MobileNumber", ticket.MobileNumber),
                new("@Email", ticket.Email),
                new("@Description", ticket.Description),
                new("@Priority", ticket.Priority),
               new("@CreatedDate",DateTime.Now),
                new("@IssueTypeId",ticket.IssueType.Id),
                new("@Status",ticket.Status.ToString())
                   };

                var rows= await _UnitOfWork.Repository<CustomerTicket>().AddAsync("InsertTickets", parameters);

                if (rows > 0)
                {
                    return Result.Success(true);
                }

                return Result.Failure<bool>(new Error
                {
                    Title = "Insert failed",
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(new Error
                {
                    Title = $"Error {ex}while saving ticket",
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
        }

        public async Task<Result<List<TicketDto>>> GetTicketList()
        {

            var tickets = await _UnitOfWork.Repository<CustomerTicket>().GetAllAsync("SelectListTicket");
            if (tickets == null || tickets.Count == 0)
            {
                return Result.Failure<List<TicketDto>>(new Error
                {
                    Title = "Empty ticket list",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            var ReturnTickets = _mapper.Map<List<TicketDto>>(tickets);
            return Result.Success<List<TicketDto>>(ReturnTickets);
        }

        public async Task<Result<List<TicketDto>>> FilterByIssueAndProperty(TicketFilterParameters parameters)
        {
            try
            {
                if (parameters == null)
                {
                    return Result.Failure<List<TicketDto>>(new Error
                    {
                        Title = "invalid parameters",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }

                var filterParams = new SqlParameter[]
            {
new ("@Priority", parameters.Priority ?? (object)DBNull.Value),
new("@IssueTypeId", parameters.IssueTypeId??(object)DBNull.Value)
               
                    };

                var tickets = await _UnitOfWork.Repository<CustomerTicket>().FilterAsync("FilterTicket", filterParams);
                var dtos = _mapper.Map<List<TicketDto>>(tickets);

                return Result.Success(dtos);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<TicketDto>>(new Error
                {
                    Title = $"Error {ex} failed to filter",
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
        }

        public  Result<TicketDto> GetTicketById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Result.Failure<TicketDto>(new Error
                    {
                        Title = "Invalid ID",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }

                var ticket =  _UnitOfWork.Repository<CustomerTicket>().GetById(id, "SelectSingleTicket");
                if (ticket == null)
                {
                    return Result.Failure<TicketDto>(new Error
                    {
                        Title = "Ticket not found",
                        StatusCode = StatusCodes.Status404NotFound
                    });
                }

                return Result.Success(_mapper.Map<TicketDto>(ticket));
            }
            catch (Exception ex)
            {
                return Result.Failure<TicketDto>(new Error
                {
                    Title = $"Error {ex} while retriving the ticket",
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
        }

        public async Task<Result<bool>> EditTicket(TicketDto ticket)
        {
            try
            {
                if (ticket == null)
                {
                    return Result.Failure<bool>(new Error
                    {
                        Title = "Invalid ticket data",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }

                var parameters = new SqlParameter[]
                {
                new("@ID", ticket.Id), 
                new("@fullName", ticket.FullName),
                new("@MobileNumber", ticket.MobileNumber),
                new("@Email", ticket.Email),
                new("@Description", ticket.Description),
                new("@Priority", ticket.Priority),
                new("@CreatedDate", DateTime.Now),
                new("@IssueTypeID",ticket.IssueType.Id),
                new("@Status",ticket.Status)
                };

                var affectedRows = await _UnitOfWork.Repository<CustomerTicket>().UpdateAsync("UpdateTicket", parameters);

                if (affectedRows < 1)
                {
                    return Result.Failure<bool>(new Error
                    {
                        Title = "failed to update",
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
                }
                return Result.Success(true);

            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(new Error
                {
                    Title = $"Error {ex} while updating object",
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
        }

       public Result<TicketDto> SearchForTicket(string name)
        {
            try
            {
                if (name ==null ||name=="")
                {
                    return Result.Failure<TicketDto>(new Error
                    {
                        Title = "Invalid name",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }

                var ticket = _UnitOfWork.Repository<CustomerTicket>().SearchTicket("SearchTicket", name);
                if (ticket == null)
                {
                    return Result.Failure<TicketDto>(new Error
                    {
                        Title = "Ticket not found",
                        StatusCode = StatusCodes.Status404NotFound
                    });
                }

                return Result.Success(_mapper.Map<TicketDto>(ticket));
            }
            catch (Exception ex)
            {
                return Result.Failure<TicketDto>(new Error
                {
                    Title = $"Error {ex} while retriving the ticket",
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
        }

    }

}

