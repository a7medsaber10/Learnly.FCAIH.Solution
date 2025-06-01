using Learnly.APIs.Errors;
using Learnly.APIs.Extensions;
using Learnly.APIs.Helpers;
using Learnly.APIs.MiddleWare;
using Learnly.Core.Entities.Identity;
using Learnly.Core.Repositories.Contract;
using Learnly.Repository;
using Learnly.Repository.Data;
using Learnly.Repository.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Learnly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Dependaancy Injection
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices(); // extension method

            // Register StoreContext Service to Store Data in DataBase.
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Register AppIdentityDbContet Service to Store Data in DataBase.
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            
            builder.Services.AddApplicationServices (); // Extension method

            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddAuthentication().AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"] ?? string.Empty))
                };

            });

            #endregion

            var app = builder.Build();

            // Ask CLR Explicitly to create object from StoreContext
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<StoreContext>();
            var _IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            var _userManager = services.GetRequiredService<UserManager<AppUser>>();


            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync(); // Update Database

                await StoreContextSeed.SeedAsync(_dbContext); // Data Seeding

                await _IdentityDbContext.Database.MigrateAsync(); // update Identity Database

                await AppIdentityDbContextSeed.SeedUserAsync(_userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Migrtion");
            }

            app.UseMiddleware<ExceptionMiddleWare>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare(); // extension method
            }

            // if I don't have an end point it redirects to errors

            //app.UseStatusCodePagesWithRedirects("/Errors/{0}");

            // we used ReExecute instead of Redirects to handle request to be one request in the network in the browser
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
