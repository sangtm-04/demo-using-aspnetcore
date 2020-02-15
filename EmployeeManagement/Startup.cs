using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.Security;
using EmployeeManagement.Services.Handlers;
using EmployeeManagement.Services.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "428905431881-9ll44ttc144dsjot6v10g9b30r9kp2us.apps.googleusercontent.com";
                    options.ClientSecret = "9tl_tkrEVVdODuNsccc8hy5v";
                    options.CallbackPath = new PathString("/Account/ExternalLoginCallback");
                })
                .AddFacebook(options =>
                {
                    options.AppId = "1451127728395370";
                    options.AppSecret = "06c056e5090c7f6b30c5c91ab96f35ed";
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role", "true"));
                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
                options.AddPolicy("CreateRolePolicy",
                    policy => policy.RequireClaim("Create Role", "true"));
                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));
                options.AddPolicy("AtLeast21", policy =>
                    policy.Requirements.Add(new MinimumAgeRequirement(21)));
            });

            services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
            services.AddScoped<IRoleCompanyRepository, SqlRoleCompanyRepository>();
            services.AddScoped<IUserCompanyRepository, SqlUserCompanyRepository>();
            services.AddScoped<ICompanyRepository, SqlCompanyRepository>();
            services.AddScoped<IUserRoleCompanyRepository, SqlUserRoleCompanyRepository>();

            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;

                // Không có company code
                endpoints.MapControllerRoute(
                    name: "NoCompany",
                    pattern: "{controller=Company}/{action=List}/{id?}",
                    constraints: new
                    {
                        controller = "Company|Error|Account"
                    }
                );

                // Các link trang nghiệp vụ
                endpoints.MapControllerRoute(
                    name: "Business",
                    pattern: "{companyCode}/{controller=Company}/{action=List}/{id?}"

                );
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
