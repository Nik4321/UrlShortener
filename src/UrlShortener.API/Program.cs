﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using UrlShortener.Data;
using UrlShortener.Data.Extensions;
using UrlShortener.Data.Models.Entities;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace UrlShortener.API
{
    /// <summary>
    /// The main enty point
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// Main entry point for the web service
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetService<UrlShortenerDbContext>();
                var roleManager = services.GetService<RoleManager<UserRole>>();
                try
                {
                    db.SeedDatabase(roleManager).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    var logger = services.GetService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                    return;
                }
            }

            host.Run();
        }

        /// <summary>
        /// Creates the web host builder
        /// </summary>
        /// <param name="args">the main args</param>
        /// <returns>An instance of <see cref="IWebHostBuilder"/></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
