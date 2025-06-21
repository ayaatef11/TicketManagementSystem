using System;
using System.Collections; 
using System.Threading.Tasks;
using TicketManagement.BLL.Interfaces.Repositories;
using TicketManagement.BLL.Interfaces.UnitOfWork;
using TicketManagement.DAL.Data.Store;
using TicketManagement.DAL.Models.Common;

namespace TicketManagement.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Hashtable _repositories =new Hashtable();
        private bool _disposed = false;
        private readonly TicketContext _ticketContext;
        public UnitOfWork(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var key = typeof(T).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _ticketContext);

                _repositories.Add(key, repository);
            }

            return (GenericRepository<T>)_repositories[key]!;
        }


        public async Task<int> CompleteAsync() => await _ticketContext.SaveChangesAsync();


        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_ticketContext != null)
            {
                await _ticketContext.DisposeAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _ticketContext?.Dispose();
                }

                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}

