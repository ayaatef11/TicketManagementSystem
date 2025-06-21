
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketManagement.BLL.Interfaces.Repositories;
using TicketManagement.DAL.Data.Store;
using TicketManagement.DAL.Models.Common;
namespace TicketManagement.BLL.Repositories
{
    internal class GenericRepository<T>: IGenericRepository<T> where T :   BaseEntity
    {
        private readonly TicketContext _ticketContext;
        public GenericRepository(TicketContext ticketContext)
        {
            _ticketContext = ticketContext ?? throw new ArgumentNullException("Ticket context is empty");
        }
            #region private methods

        private static bool IsValidProcedure(string procedure)
        {
            var procedures = new HashSet<string> { "SelectSingleTicket", "UpdateTicket", "SelectListTicket", "InsertTickets", "FilterTicket", "SelectIssueType", "SearchTicket" };
            return procedures.Contains(procedure);
        }
#endregion
        public async Task<bool> DeleteAsync(string procedure, params SqlParameter[] parameters)
        {
            if (!IsValidProcedure(procedure))
            {
                throw new ArgumentException("invalid procedure name", nameof(procedure));
            }
            return  await _ticketContext.Database.ExecuteSqlRawAsync(procedure, parameters)>0;
        } 
        public async Task<int> UpdateAsync(string procedure, params SqlParameter[] parameters)
        {
            if (!IsValidProcedure(procedure))
            {
                throw new ArgumentException("invalid procedure name", nameof(procedure));
            }
            var paramNames = string.Join(", ", parameters.Select(p => $"{p.ParameterName}"));
            string sql = $"EXEC {procedure} {paramNames}";

            return await _ticketContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<List<T>> GetAllAsync(string procedure)
        {
            if (!IsValidProcedure(procedure))
            {
                throw new ArgumentException("invalid procedure name", nameof(procedure));
            }
            return await _ticketContext.Set<T>().FromSqlRaw(procedure).AsNoTracking().ToListAsync();
        }

        public T/*async Task<T>*/ GetById(int id, string procedure)  
        {
            if (!IsValidProcedure(procedure))
            {
                throw new ArgumentException("invalid procedure name", nameof(procedure));
            }

            var ticket =  _ticketContext.Set<T>().FromSqlRaw($"EXEC {procedure} @id", new SqlParameter("id", id)).AsEnumerable().FirstOrDefault();//.FirstOrDefaultAsync();
            return ticket;
        }

        public async Task<int> AddAsync(string procedure, params SqlParameter[] parameters)
        {
            if (!IsValidProcedure(procedure))
            {
                throw new ArgumentException("invalid procedure name", nameof(procedure));
            }
            var paramNames = string.Join(", ", parameters.Select(p => $"{p.ParameterName}"));
            string sql = $"EXEC {procedure} {paramNames}";

            return await _ticketContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<List<T>> FilterAsync(string procedure, params SqlParameter[] parameters) 
        {
            if (!IsValidProcedure(procedure))
            {
                throw new ArgumentException("invalid procedure name", nameof(procedure));
            }
            var paramNames = string.Join(", ", parameters.Select(p => $"{p.ParameterName}"));
            var sql = $"EXEC {procedure} {paramNames}";

            return await _ticketContext.Set<T>().FromSqlRaw(sql, parameters).AsNoTracking().ToListAsync();
        }
       public T SearchTicket(string procedure, string name)
        {
            if (!IsValidProcedure(procedure))
            {
                throw new ArgumentException("invalid procedure name", nameof(procedure));
            }

            var ticket = _ticketContext.Set<T>().FromSqlRaw($"EXEC {procedure} @name", new SqlParameter("name", name)).AsEnumerable().FirstOrDefault();
            return ticket;
        }

    }
}
