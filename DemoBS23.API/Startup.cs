using DemoBS23.BLL.Services.DemoShopService.CustomerService;
using DemoBS23.BLL.Services.DemoShopService.OrderService;
using DemoBS23.BLL.Services.DemoShopService.ProductService;
using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Repositories.DemoShop;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoBS23.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                var currentVersion = Configuration.GetValue<string>("ApiVersion:currentVersion");
                options.SwaggerDoc(currentVersion,
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "BrainStation-23 Demo API",
                        Description = "This is a demo api by Hamid",
                        Version = currentVersion
                    });
                var fileName = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                options.IncludeXmlComments(filePath);

            });

            #region DbContext DI
            //services.AddDbContext<appDbContext>();
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("db_bs23_demo"));
            });
            #endregion

            #region Repositories DI
            //services.AddScoped<IProductRepo, ProductRepo>();
            //services.AddScoped<IDemoShopRepo, DemoShopRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();

            #endregion

            #region Services DI
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IDemoShopService, DemoShopService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();


            #endregion

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options=>
            {
                var currentVersion = Configuration.GetValue<string>("ApiVersion:currentVersion");
                options.SwaggerEndpoint($"/swagger/{currentVersion}/swagger.json", "Swagger Demo API for BS23_Demo_API");
            });
        }
    }
}
