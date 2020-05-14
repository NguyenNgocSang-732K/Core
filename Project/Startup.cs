using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Models.Entities;
using System.Security.Claims;

namespace Project
{
    public class Startup
    {

        private IConfiguration configuration { get; }

        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DBContext>(option => option.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("ConnectDB")));
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "SCHEME_ADMIN";
            })
            .AddCookie("SCHEME_ADMIN", option =>
             {
                 option.LoginPath = "/admin/login";
                 option.AccessDeniedPath = "/admin/accessDenied";
                 option.LogoutPath = "/admin/logout";
                 option.Cookie.Name = "dumaloangngoangvailon";
             })
            .AddCookie("SCHEME_USER", option =>
            {
                option.LoginPath = "/user/login";
                option.AccessDeniedPath = "/user/accessDenied";
                option.LogoutPath = "/user/logout";
                option.Cookie.Name = "dumaloangngoangvailon";
            });
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseStaticFiles();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                ClaimsPrincipal principal = new ClaimsPrincipal();
                var result = await context.AuthenticateAsync("SCHEME_ADMIN");
                if (result?.Principal != null)
                {
                    principal.AddIdentities(result.Principal.Identities);
                }
                var result3 = await context.AuthenticateAsync("SCHEME_USER");
                if (result3?.Principal != null)
                {
                    principal.AddIdentities(result3.Principal.Identities);
                }
                context.User = principal;
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin_route", "admin/{controller}/{action}/{id?}",
                    defaults: new { area = "admin" }, constraints: new { area = "admin" });

                endpoints.MapControllerRoute("user_route", "user/{controller}/{action}/{id?}",
                    defaults: new { area = "user" }, constraints: new { area = "user" });

                endpoints.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
