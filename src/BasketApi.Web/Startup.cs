﻿using System;
using System.IO;
using System.Reflection;
using BasketApi.Infrastructure;
using BasketApi.Infrastructure.Interfaces;
using BasketApi.Infrastructure.Repos;
using BasketApi.Web.Interfaces;
using BasketApi.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace BasketApi.Web
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Method that is called by the runtime when the API is on development
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            ConfigureInMemoryDatabases(services);

            ConfigureServices(services);
        }

        /// <summary>
        /// Method that is called by the runtime when the API is on production
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use in-memory database. 
            // improvement to configure a persistent db like sql server
            ConfigureInMemoryDatabases(services);

            ConfigureServices(services);
        }

        /// <summary>
        /// Method to configure in memory databases
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<BasketDbContext>(c =>
                c.UseInMemoryDatabase(databaseName: "BasketDB"));
        }

        /// <summary>
        /// Method that is called by runtime in order to add services to the container
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddMemoryCache();

            services.AddMvc()
                .AddJsonOptions(options =>{ options.SerializerSettings.Formatting = Formatting.Indented; })
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            // Register the Swagger generator for the documentation
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Basket API",
                    Version = "v1",
                    Description = "Basket example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Pantelis Chatzimichalis",
                        Email = string.Empty,
                        Url = "https://www.linkedin.com/in/pantelischatzimichalis/"
                    }
                });

                options.IncludeXmlComments(GetXmlCommentsPath());
                options.DescribeAllEnumsAsStrings();
            });

            _services = services;
        }

        private static string GetXmlCommentsPath()
        {
            return Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml");
        }

        /// <summary>
        /// Method that is called by the runtime in order to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App builder</param>
        /// <param name="env">Hosting envinroment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
                app.UseExceptionHandler("/Error");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
