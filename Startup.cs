using MusicConcert.Data;
using MusicConcert.Models;
using MusicConcert.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicConcert
{
    public class Startup
    {
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // Database Configuration
            services.AddDbContext<EventContext>(
                options => options.UseSqlServer("Server=.;Database=Events;Integrated Security=True;"));


            //database conf for Identity
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EventContext>();

            // Password Custom Requirements - configuring
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            // Redirect to login page if not logged-In
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/login";
            });
            

            // using dependency injection
            services.AddScoped<IEventRepository, EventRepository>();

            // for user login-signup
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // for static files using
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapDefaultControllerRoute(); 

            });

           
        }
    }
}
