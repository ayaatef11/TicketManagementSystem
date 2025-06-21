using System.Threading.Tasks;
using System;
using TicketManagement.DAL.Models.Common;
using TicketManagement.BLL.Interfaces.Repositories;

namespace TicketManagement.BLL.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
    }
}

