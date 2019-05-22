using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberMe.Models;
using BarberMe.Models.Classes;
using BarberMe.Models.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project.ver1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
           Configuration = configuration;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:Users:ConnectionString"]));

            services.AddIdentity<BarbershopUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration["Data:Services:ConnectionString"]));
            services.AddTransient<IServiceRepository, ServiceRepository>();

            //services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});

            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default",
                                template: "{controller=Home}/{action=Index}");
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
