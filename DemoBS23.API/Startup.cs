using DemoBS23.BLL.AppConfig;
using DemoBS23.BLL.Services.Admin;
using DemoBS23.BLL.Services.Auth;
using DemoBS23.BLL.Services.DemoShopService.CustomerService;
using DemoBS23.BLL.Services.DemoShopService.OrderService;
using DemoBS23.BLL.Services.DemoShopService.ProductService;
using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Entities.Auth;
using DemoBS23.DAL.Repositories.Admin;
using DemoBS23.DAL.Repositories.Auth;
using DemoBS23.DAL.Repositories.DemoShop;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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
            #region AppConfigurations
            var jwtConfigSection = Configuration.GetSection("JwtConfig");
            services.Configure<JwtConfig>(jwtConfigSection);
            var jwtConfigObj = jwtConfigSection.Get<JwtConfig>();

            var authSecretKey = Encoding.ASCII.GetBytes(jwtConfigObj.Secret);

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(authSecretKey),
                ValidateIssuer = true,
                ValidIssuer = jwtConfigObj.ValidIssuer,
                ValidateAudience = true,
                ValidAudience = jwtConfigObj.ValidAudience,
                ValidateLifetime = true,

                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParams);
            #endregion

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

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
            
            services.AddDbContext<AuthDbContext>(options =>
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
            services.AddScoped<IAuthRepo, AuthRepo>();
            services.AddScoped<IAdminRepo, AdminRepo>();
            #endregion

            #region Services DI
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IDemoShopService, DemoShopService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            #endregion

            services.AddAutoMapper(typeof(Startup));


            services.AddIdentity<AuthUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions => {
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = tokenValidationParams; });

            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader() );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options=>
            {
                var currentVersion = Configuration.GetValue<string>("ApiVersion:currentVersion");
                options.SwaggerEndpoint($"/swagger/{currentVersion}/swagger.json", "BS23_Demo_API by Hamidur");
            });
        }
    }
}
