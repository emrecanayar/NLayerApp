using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NLayerApp.API.Filters;
using NLayerApp.Core.Configurations;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Models;
using NLayerApp.Repository.Contexts;
using NLayerApp.Repository.Logs;
using NLayerApp.Service.Helpers;
using NLayerApp.Service.Services;
using System.Reflection;

namespace NLayerApp.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, string connectionString, ConfigurationManager configuration)
        {
            services.AddScoped(typeof(NotFoundFilter<>));
            services.AddScoped<CurrentUserService>();
            services.AddMemoryCache();
            services.AddDataProtection();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            #region Configure
            services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));
            services.Configure<IpList>(configuration.GetSection("IpList"));
            #endregion

            #region Cors Policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSites", builder =>
                {
                    builder.WithOrigins("https://localhost:44313", "https://www.mysite.com").AllowAnyHeader().AllowAnyMethod();
                });

                options.AddPolicy("AllowSites2", builder =>
                {
                    builder.WithOrigins("https://www.mysite1.com", "https://www.mysite2.com").WithHeaders(HeaderNames.ContentType, "x-custom-header");
                });

                options.AddPolicy("AllowSitesAndSubDomains", builder =>
                {
                    builder.WithOrigins("https://*.example.com").SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod();
                });

                options.AddPolicy("AllowSitesMethods", builder =>
                {
                    builder.WithOrigins("https://localhost:44313").WithMethods("POST", "GET").AllowAnyHeader();
                });
            });
            #endregion


            services.AddDbContext<ApplicationDbContext>(
            options =>
            options.LogTo(msg => EntityFrameworkQueryLog.LogQuery(msg), LogLevel.Information).UseSqlServer(connectionString,
            options =>
            options.MigrationsAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)).GetName().Name)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.User.AllowedUserNameCharacters = configuration.GetSection("AllowedUserNameCharacters").Value;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],
                    IssuerSigningKey = SignServiceHelper.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

    }
}
