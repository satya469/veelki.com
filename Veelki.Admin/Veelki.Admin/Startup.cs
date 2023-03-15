using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Veelki.Core.IServices;
using Veelki.Core.Services;
using Veelki.Data;
using Veelki.Data.Entities;
using Veelki.Data.Infrastructure;
using Veelki.Data.Repository;
using Veelki.Data.UOW;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Veelki.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            

            services.AddDbContext<RB444Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<RB444Context>();

            services.AddMvc();
            ////Set Session Timeout. Default is 20 minutes.
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //});

            services.AddTransient<IUserStore<Users>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

            services.AddIdentity<Users, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<IDatabase, Database>();

            services.AddTransient<IRequestServices, RequestServices>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<ICookieService, CookieService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {               
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var supportedCultures = new List<CultureInfo> { };
            var gb = new CultureInfo("en-US");
            gb.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy HH:mm:ss";
            gb.DateTimeFormat.DateSeparator = "/";
            supportedCultures.Add(gb);

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures,
            });
            app.Use(async (context, next) =>
            {
                context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-US"))
                );
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}
