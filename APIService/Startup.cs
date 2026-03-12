using APIService.Connection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SAPbobsCOM;
using System;
using System.Text;

namespace APIService
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
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            #region CORS

            services.AddCors(options =>
            {
                options.AddPolicy("AllowWebService",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5003")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            #endregion

            #region AddScope

            #endregion

            #region ConfigureJWTToken
//            Console.WriteLine("SECRET = " + Configuration["Secret"]);
//            var tokenvalidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey =
//                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY_SUPER_ULTRA_SECRET_KEY_FOR_JWT_TOKEN_2026_PROJECT")),
//                ValidateIssuer = false,
//                ValidateAudience = false,
//                RequireExpirationTime = true,
//                ValidateLifetime = true,
//                ClockSkew = TimeSpan.FromDays(10)
//            };
//            services.AddSingleton(tokenvalidationParameters);
//            services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(options =>
//            {
//                options.TokenValidationParameters = tokenvalidationParameters;

//                options.Events = new JwtBearerEvents
//                {
//                    OnMessageReceived = context =>
//                    {
//                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
//                        Console.WriteLine(
//    $"[{context.Request.Method}] {context.Request.Path} Authorization: {authHeader}"
//);
//                        if (string.IsNullOrEmpty(authHeader))
//                        {
//                            // ไม่มี token → ข้าม
//                            return Task.CompletedTask;
//                        }

//                        Console.WriteLine("Authorization Header: " + authHeader);

//                        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
//                        {
//                            context.Token = authHeader.Substring("Bearer ".Length).Trim();
//                        }

//                        return Task.CompletedTask;
//                    },

//                    OnAuthenticationFailed = context =>
//                    {
//                        Console.WriteLine("JWT ERROR: " + context.Exception);
//                        return Task.CompletedTask;
//                    }
//                };
//            });

//            services.AddAuthorization();

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            #endregion

            #region SAP Service

            services.AddSingleton<ISapConnection, SapConnection>();
            services.AddSingleton<SapConnectionPool>();
            services.AddScoped<SapOrderService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIService v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowWebService");
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            LoadConnectionConfig();
            /*SapDriverOCompany.Init_oCompany();*/
        }

        private void LoadConnectionConfig()
        {
            ConnectionString.DbServerType = Configuration["DbServerType"];
            ConnectionString.Server = Configuration["Server"];
            ConnectionString.LicenseServer = Configuration["LicenseServer"];
            ConnectionString.SLDServer = Configuration["SLDServer"];
            ConnectionString.DbUserName = Configuration["DbUserName"];
            ConnectionString.DbPassword = Configuration["DbPassword"];
            ConnectionString.CompanyDB = Configuration["CompanyDB"];
            ConnectionString.UserName = Configuration["UserNameSAP"];
            ConnectionString.Password = Configuration["Password"];
            ConnectionString.ConnHana = Configuration["ConnectionStringHANA2"];
            ConnectionString.PronWebDB = Configuration["PronWebDB"];
            ConnectionString.ServerGET = Configuration["ServerGET"];
            ConnectionString.ServerGETName = Configuration["ServerGETName"];
        }
    }
}