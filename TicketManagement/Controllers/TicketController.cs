using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.BLL.Interfaces.Services;
using TicketManagement.DAL.DTOS;
using TicketManagement.DAL.Enums;
using TicketManagement.DAL.Parameters;
using TicketManagement.ViewModels.Errors;
using TicketManagement.ViewModels.Requests;
using TicketManagement.ViewModels.Responses;

namespace TicketManagement.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketsService _ticketsService;
        private readonly IMapper _mapper;
        private readonly IIssueTypesService _issueTypesService;
        private readonly ILogger _logger;
        public TicketController(ITicketsService ticketsService,IMapper mapper,IIssueTypesService issueTypeService,ILogger<TicketController>logger)
        {
          _ticketsService = ticketsService;  
            _mapper = mapper;
            _issueTypesService = issueTypeService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet] 
        
        public async Task< IActionResult> FilterTickets(TicketFilterViewModelRequest request)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(request); 
            //}
            try
            {
                var parameters = _mapper.Map<TicketFilterParameters>(request);
                var result = await _ticketsService.FilterByIssueAndProperty(parameters);
                if (!result.IsSuccess)
                {
                    return RedirectToAction("Error", new
                    {
                        title = "Failed to filter",
                        message = result.Error.Title,
                        statusCode = result.Error.StatusCode
                    });
                }
                //var issueTypes = await _issueTypesService.GetAllAsync();
            
                List<TicketViewModelResponse> response = _mapper.Map<List<TicketViewModelResponse>>(result.Value);
                return View(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading tickets");
                return RedirectToAction("Error", new
                {
                    title = "Failed select the  ticket",
                    message = "Error while viewing tickets",
                    statusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> InsertTicket()
        {
            try
            {
                var issueTypes = await _issueTypesService.GetAllAsync();
                ViewBag.IssueTypeList = issueTypes
                    .Select(it => new SelectListItem { Value = it.Id.ToString(), Text = it.IssueTypeName })
                    .ToList();
                return View(new TicketViewModelResponse()
                {
                    Status = Status.open
                });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error viewing insert ticket page ");
                return RedirectToAction("Error", new
                {
                    title = "Failed view insert ticket page",
                    message = "Error while viewing tickets",
                    statusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> InsertTicket(TicketViewModelRequest request)
        {
            try
            {
                TicketDto ticket = _mapper.Map<TicketDto>(request);
                var result = await _ticketsService.SaveTicket(ticket);
                if (!result.IsSuccess)
                {
                    return RedirectToAction("Error", new
                    {
                        title = "Failed to submit",
                        message = result.Error.Title,
                        statusCode = result.Error.StatusCode
                    });
                }
                TempData["Success"] = "Ticket submitted successfully!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading tickets");
                return RedirectToAction("Error", new
                {
                    title = "Failed select the  ticket",
                    message = "Error while viewing tickets",
                    statusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> SelectAllTickets(List<TicketViewModelResponse>response)
        {
            try
            {
                if (response.Count == 0)
                {
                    var result = await _ticketsService.GetTicketList();
                    if (!result.IsSuccess)
                    {
                        return RedirectToAction("Error", new
                        {
                            title = "Failed to view tickets",
                            message = result.Error.Title,
                            statusCode = result.Error.StatusCode
                        });
                    }
                    var issueTypes = await _issueTypesService.GetAllAsync();
                    ViewBag.IssueTypeList = issueTypes
                        .Select(it => new SelectListItem { Value = it.Id.ToString(), Text = it.IssueTypeName })
                        .ToList();
                    response = _mapper.Map<List<TicketViewModelResponse>>(result.Value);
                }
                return View(response);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error loading tickets");
                return RedirectToAction("Error", new
                {
                    title = "Failed select the  ticket",
                    message = "Error while viewing tickets",
                    statusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
        
        [HttpGet("SelectSingleTicket/{id}")]
        public  IActionResult SelectSingleTicket(int id )
        {
            if (id <= 0)
            {
                return RedirectToAction("Error", new
                {
                    title = "invalid request",
                    message ="id is not valid",
                    statusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = _ticketsService.GetTicketById(id);

                if (!result.IsSuccess)
                {
                    return RedirectToAction("Error", new
                    {
                        title = "Failed select the  ticket",    
                        message = result.Error.Title,
                        statusCode = result.Error.StatusCode
                    });
                }

                TicketViewModelResponse response = _mapper.Map<TicketViewModelResponse>(result.Value);
                return View(response);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error loading ticket ",id);
                return RedirectToAction("Error", new
                    {
                        title = "Failed select the  ticket",
                        message = "Error while viewing tickets",
                        statusCode = StatusCodes.Status500InternalServerError
                    });
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTicket(int id)
        {
            try
            {

                var result =  _ticketsService.GetTicketById(id);

                if (!result.IsSuccess)
                {
                    return RedirectToAction("Error", new
                    {
                        title = "Ticket Not Found",
                        statusCode = StatusCodes.Status404NotFound
                    });
                }

                var response = _mapper.Map<TicketViewModelResponse>(result.Value);
                var issueTypes = await _issueTypesService.GetAllAsync();
                ViewBag.IssueTypeList = issueTypes
                    .Select(it => new SelectListItem { Value = it.Id.ToString(), Text = it.IssueTypeName })
                    .ToList();
                return View(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading edit form for ticket {TicketId}", id);
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTicket(TicketViewModelRequest request)
        {
//            if (!ModelState.IsValid)
//            {
//var response=_mapper.Map<TicketViewModelResponse>(request);
//                return View(response);  
//            }
            try
            {
                var parameters = _mapper.Map<TicketDto>(request);
                var result = await _ticketsService.EditTicket(parameters);
                if (!result.IsSuccess)
                {
                    return RedirectToAction("Error", new
                    {
                        title = "Failed to update ticket",
                        message = result.Error.Title,
                        statusCode = result.Error.StatusCode
                    });
                }
                TempData["Success"] = "Ticket updated successfully!";
                return RedirectToAction(nameof(SelectSingleTicket), new { request.Id });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error while updating ticket", request.Id);
                return RedirectToAction("Error", new { statusCode = 500 });
            }
        }
    

        public IActionResult Error(string title, string message, int statusCode)
        {
            var errorViewModel = new TicketErrorResponseViewModel
            {
                Title = title,
                Message = message,
                StatusCode = statusCode
            };
            return View(errorViewModel);
        }

        [HttpGet("SearchTicket")]
        public IActionResult SearchTicket(string searchTerm)
        {
            if (searchTerm == null || searchTerm == "")
            {
                return RedirectToAction("Error", new
                {
                    title = "invalid request",
                    message = "name is not valid",
                    statusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = _ticketsService.SearchForTicket(searchTerm);

                if (!result.IsSuccess)
                {
                    return RedirectToAction("Error", new
                    {
                        title = "Failed select the  ticket",
                        message = result.Error.Title,
                        statusCode = result.Error.StatusCode
                    });
                }

                TicketViewModelResponse response = _mapper.Map<TicketViewModelResponse>(result.Value);
                return RedirectToAction(nameof(SelectSingleTicket), new { id = response.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading ticket ", searchTerm);
                return RedirectToAction("Error", new
                {
                    title = "Failed select the  ticket",
                    message = "Error while viewing ticket",
                    statusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

    }
}
