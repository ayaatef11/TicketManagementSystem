
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace TicketManagement.BLL.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<bool> DeleteAsync(string sql, params SqlParameter[] parameters);
        Task<int> UpdateAsync(string sql, params SqlParameter[] parameters);
        Task<List<T>> GetAllAsync(string procedure);

        T GetById(int id, string procedure);

        Task<int> AddAsync(string procedure, params SqlParameter[] parameters);
        Task<List<T>> FilterAsync(string procedure, params SqlParameter[] parameters);

        T SearchTicket(string procedure, string name);
    }
}
