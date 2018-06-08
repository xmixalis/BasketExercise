﻿using System;
//using BasketCore;
//using BasketCore.Interfaces;
//using BasketWebUI.Api.Interfaces;
//using BasketWebUI.Api.Services;
using BasketWebUI.Interfaces;
using BasketWebUI.Services;
//using BasketApi.Infrastructure;
//using BasketApi.Infrastructure.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketWebUI
{
    public class Startup
    {
        private IServiceCollection _services;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /*
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            ConfigureInMemoryDatabases(services);

            ConfigureServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<BasketDbContext>(c =>
                c.UseInMemoryDatabase(databaseName: "BasketDB"));
        }
        */
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "";
                options.LogoutPath = "";
                options.Cookie = new CookieBuilder
                {
                    // required for auth to work without explicit user consent;
                    IsEssential = true 
                };
            });

//            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
 //           services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IBasketWebService, BasketService>();
            services.AddScoped<IProductService, ProductsService>();

//            services.AddScoped<IBasketApiService, BasketApiService>();
            // Add memory cache services
            services.AddMemoryCache();

            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}