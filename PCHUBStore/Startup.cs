using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using PCHUBStore.Services;
using PCHUBStore.MiddlewareFilters;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Services.EmailSender;
using PCHUBStore.Areas.Administration.Services;
using PCHUBStore.Hubs;
using PCHUBStore.Areas.Support.Services;

namespace PCHUBStore
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<PCHUBDbContext>(options =>

             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
             b => b.MigrationsAssembly("PCHUBStore"))
                                                     .UseLazyLoadingProxies()
                                                  );
            services.AddSignalR(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
            });

            services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromDays(20); options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true; });
            services.AddResponseCaching();
            services.AddResponseCompression(options => { options.EnableForHttps = true; });
            services.AddRazorPages();

            services.AddIdentity<User, IdentityRole>(options =>
            options.User.RequireUniqueEmail = true
            )
                .AddEntityFrameworkStores<PCHUBDbContext>()
                .AddDefaultTokenProviders();



            Account cloudinaryCredentials = new Account(
              this.Configuration["Cloudinary:CloudName"],
              this.Configuration["Cloudinary:ApiKey"],
              this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton(cloudinaryUtility);

            services.AddScoped<ValidationFilter>();

            services.AddTransient<IProductServices, ProductServices>();

            services.AddTransient<ICloudinaryServices, CloudinaryServices>();

            services.AddTransient<IUserProfileServices, UserProfileServices>();

            services.AddTransient<IHomeService, HomeService>();

            services.AddTransient<IAdminProductsServices, AdminProductsServices>();

            services.AddTransient<IAdminCharacteristicsServices, AdminCharacteristicsServices>();

            services.AddTransient<IAdminFiltersServices, AdminFiltersServices>();

            services.AddTransient<IAdminIndexPageServices, AdminIndexPageServices>();

            services.AddTransient<IAdminCategoryPagesServices, AdminCategoryPagesServices>();

            services.AddTransient<ICategoryServices, CategoryServices>();

            services.AddTransient<IShopServices, ShopServices>();

            services.AddTransient<IAdminLayoutServices, AdminLayoutServices>();

            services.AddTransient<IRequestChatServices, RequestChatServices>();

            services.AddTransient<ISupportForumServices, SupportForumServices>();

            services.AddTransient<IShipmentManagerServices, ShipmentManagerServices>();

            services.AddTransient<IForumServices, ForumServices>();

            services.AddTransient<ISupportChartsService, SupportChartsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            using var scoperService = app.ApplicationServices.CreateScope();

            var roleManager = scoperService.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            var adminRole = roleManager.FindByNameAsync("Admin").GetAwaiter().GetResult();

            var userRole = roleManager.FindByNameAsync("StoreUser").GetAwaiter().GetResult();

            var supportRole = roleManager.FindByNameAsync("Support").GetAwaiter().GetResult();

            if (adminRole == null)
            {
                roleManager.CreateAsync(new IdentityRole
                {

                    Name = "Admin"

                }).GetAwaiter().GetResult();
            }

            if (userRole == null)
            {
                roleManager.CreateAsync(new IdentityRole
                {

                    Name = "StoreUser"

                }).GetAwaiter().GetResult();
            }

            if (supportRole == null)
            {
                roleManager.CreateAsync(new IdentityRole
                {

                    Name = "Support"

                }).GetAwaiter().GetResult();
            }

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
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapHub<RequestChatHub>("/requestChat");


                endpoints.MapControllerRoute(
                    "areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapRazorPages();
            });


        }
    }
}
