using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using ThemeParkDatabase.Data;
using ThemeParkDatabase.Services;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
            IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Employee" };
            IdentityResult RR;
            foreach(var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    RR = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
                    options.AddPolicy("Employee", policy => policy.RequireRole("Employee"));
                });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    
                    options.Conventions.AllowAnonymousToPage("/Account/Login");
                    /*
                    options.Conventions.AllowAnonymousToPage("/Accounts/Register");
                    options.Conventions.AllowAnonymousToPage("/Accounts/ResetPassword");
                    options.Conventions.AllowAnonymousToPage("/Accounts/ResetPasswordConfirmation");
                    /*
                    options.Conventions.AuthorizePage("/Index");
                    options.Conventions.AuthorizeFolder("/Employees");
                    options.Conventions.AuthorizeFolder("/Visitors");
                    options.Conventions.AuthorizeFolder("/Maintenance");
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                    */
                });

            var connection = @"Server=(localdb)\mssqllocaldb;Database=ThemeParkDatabase;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ThemeParkDatabaseContext>(b => b.UseSqlServer(connection));
                
                
                /*(options => options.UseSqlServer(connection));*/

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
