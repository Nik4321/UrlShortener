﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using UrlShortener.Data;
using UrlShortener.Data.Models;
using UrlShortener.Infrastructure.Settings;
using UrlShortener.Repositories;
using UrlShortener.Services;

namespace UrlShortener.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDbContext(services, configuration);
            RegisterServices(services);
            RegisterRepositories(services);
            RegisterAutoMapper(services);
            RegisterSwagger(services);
            RegisterCors(services);
            RegisterHealthCheck(services);
            RegisterIdentity(services, configuration);
            RegisterAuthorization(services);
        }

        private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<UrlShortenerDbContext>(
                    options => options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                        sqlOptions => { sqlOptions.ServerVersion(new Version(10, 1, 40), ServerType.MariaDb); }));
        }

        private static void RegisterHealthCheck(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<UrlShortenerDbContext>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUrlService, UrlService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUrlRepository, UrlRepository>();
        }

        private static void RegisterAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Url Shortener API", Version = "v1" }); });
            // Adding this to make swagger gen work.
            services.AddMvcCore().AddApiExplorer();
        }

        private static void RegisterCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }

        private static void RegisterIdentity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<UrlShortenerDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    var jwtSettings = configuration.GetSection(nameof(JwtSettings));
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings[nameof(JwtSettings.Authority)],
                        ValidAudience = jwtSettings[nameof(JwtSettings.Audience)],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings[nameof(JwtSettings.Secret)])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        private static void RegisterAuthorization(IServiceCollection services)
        {
            services.AddAuthorization();
        }
    }
}
