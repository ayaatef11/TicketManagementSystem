using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.BLL.Interfaces.Services;
using TicketManagement.BLL.Interfaces.UnitOfWork;
using TicketManagement.BLL.Services;
using TicketManagement.DAL.Data.Store;
using TicketManagement.BLL.Repositories;
namespace TicketManagement.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddAntiforgery();
            services.AddLogging();
            services.AddDbContext<TicketContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                   
            });
            services.AddScoped<ITicketsService, TicketsService>();
            services.AddScoped<IIssueTypesService, IssueTypesService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }  
    }
}
