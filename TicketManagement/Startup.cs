using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using TicketManagement.BLL.Interfaces.Services;
using TicketManagement.BLL.Services;
using TicketManagement.Extensions;

namespace TicketManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
           .AddFluentValidation(config => {
               // Automatically register validators from assemblies
               config.RegisterValidatorsFromAssemblies(new[]
               {
                    Assembly.Load("TicketManagement.DAL"),
                    Assembly.Load("TicketManagement")
               });

               // Optional: disable default MVC validation if you want FluentValidation only
               // config.DisableDataAnnotationsValidation = true;
           });
            services.AddApplicationServices(Configuration);
            services.AddAutoMapper(typeof(Startup));

    //        var assemblies = new[]
    //        {
    //Assembly.Load("TicketManagement.DAL"),
    //Assembly.Load("TicketManagement")
    //        };

    //        services.AddValidatorsFromAssemblies(assemblies);
            //services.AddValidatorsFromAssemblies(Assembly.GetExecutingAssembly());
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }); 
        }
    }
}
