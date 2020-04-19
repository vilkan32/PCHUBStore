using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CloudinaryDotNet;
using Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTested.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Services;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Hubs;
using PCHUBStore.MiddlewareFilters;
using PCHUBStore.Services;
using PCHUBStore.Services.EmailSender;

namespace PCHUBStore.Tests
{
    public class TestStartup : Startup
    {


        public IConfiguration Configuration { get; }


        public TestStartup(IConfiguration configuration) : base(configuration)
        {


        }


        public void ConfigureTestServices(IServiceCollection services)
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
            services.AddSignalR(options => { options.ClientTimeoutInterval = TimeSpan.FromMinutes(2); });

            services.AddControllersWithViews(
                options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddResponseCaching();
            services.AddResponseCompression(options => { options.EnableForHttps = true; });
            services.AddRazorPages();

            services.AddIdentity<User, IdentityRole>(options =>
                    options.User.RequireUniqueEmail = true
                )
                .AddEntityFrameworkStores<PCHUBDbContext>()
                .AddDefaultTokenProviders();




            services.AddTransient<IEmailSender, EmailSender>();

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

    }
}
