using AutoMapper;
using TicketManagement.DAL.DTOS;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Parameters;
using TicketManagement.ViewModels.Requests;
using TicketManagement.ViewModels.Responses;

namespace TicketManagement.Mapping
{
    public class TicketMapping:Profile
    {
        public TicketMapping()
        {
            
            CreateMap<TicketFilterViewModelRequest, TicketFilterParameters>();
            CreateMap<TicketViewModelRequest, TicketDto>();
            CreateMap<TicketDto, TicketViewModelResponse>();
            CreateMap<TicketInsertViewModel,TicketDto>();
            CreateMap<TicketViewModelRequest, TicketViewModelResponse>();
            CreateMap<CustomerTicket, TicketDto>();
            CreateMap<TicketDto, CustomerTicket>();
            CreateMap<TicketDto,TicketViewModelRequest>();  
        }
    }
}
