using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.BLL.Interfaces.Services;
using TicketManagement.BLL.Interfaces.UnitOfWork;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Services
{
    public class IssueTypesService: IIssueTypesService
    {
        private readonly IUnitOfWork _UnitOfWork;
        public IssueTypesService(IUnitOfWork UnitOfWork) 
        {
            _UnitOfWork = UnitOfWork;
        }
        public async Task<List<IssueType>> GetAllAsync()
        {
          return await  _UnitOfWork.Repository<IssueType>().GetAllAsync("SelectIssueType");
        }

    }
}
