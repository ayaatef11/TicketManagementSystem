using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using TicketManagement.DAL.Models;
using System;
using Microsoft.Extensions.Logging;
namespace TicketManagement.DAL.Data.Store
{
    public class TicketContext:DbContext
    {
        private readonly ILogger _logger;
        public TicketContext(DbContextOptions<TicketContext> options,ILogger<TicketContext> logger) : base(options)
        {
            _logger = logger;
        }
        public DbSet<CustomerTicket> CustomerTickets { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }   

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (Exception ex) {
                _logger.LogError($"Error {ex} while Saving Changes", DateTime.UtcNow.ToLongTimeString());

                return 0;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
             
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
