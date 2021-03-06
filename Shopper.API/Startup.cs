using Shopper.BLL.AppConfig;
using Shopper.BLL.Services.Admin;
using Shopper.BLL.Services.Auth;
using Shopper.BLL.Services.DemoShopService.CustomerService;
using Shopper.BLL.Services.DemoShopService.OrderService;
using Shopper.BLL.Services.DemoShopService.ProductService;
using Shopper.DAL.DatabaseContext;
using Shopper.DAL.Entities.Auth;
using Shopper.DAL.Repositories.Admin;
using Shopper.DAL.Repositories.Auth;
using Shopper.DAL.Repositories.DemoShop;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Shopper.API
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
                var currentName = Configuration.GetValue<string>("ApiVersion:name");

                options.SwaggerDoc(currentVersion,
                    new OpenApiInfo
                    {
                        Title = currentName,
                        Description = "An ASP.NET Core 3.1 Web Api for Shopper (Demo Online Shop)",
                        Version = currentVersion,
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Contact",
                            Url = new Uri("https://example.com/contact")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "License",
                            Url = new Uri("https://example.com/license")
                        }
                    });

                var fileName = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                options.IncludeXmlComments(filePath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name =  "Authorization",
                    Type =  SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxx.yyy.zzz\"",

                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

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

            app.UseStaticFiles();

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
                options.SwaggerEndpoint($"/swagger/{currentVersion}/swagger.json", $"Shopper - Web API v{currentVersion}");
            });
        }
    }
}
