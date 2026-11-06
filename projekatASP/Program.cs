using Application.Commands;
using Application.Commands.Center;
using Application.Commands.Court;
using Application.Commands.Reservation;
using Application.Commands.Sport;
using Application.Queries;
using DataAccess;
using Domain.Entities;
using Implementation.UseCases.Commands;
using Implementation.UseCases.Commands.Center;
using Implementation.UseCases.Commands.Centers;
using Implementation.UseCases.Commands.Court;
using Implementation.UseCases.Commands.Courts;
using Implementation.UseCases.Commands.Reservations;
using Implementation.UseCases.Commands.Sport;
using Implementation.UseCases.Queries;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using projekatASP.API;
using projekatASP.API.Extensions;
using projekatASP.API.Middleware;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace projekatASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddJwtAuthentication(builder.Configuration);
            IdentityModelEventSource.ShowPII = true;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseMiddleware <GlobalExceptionHandlingMiddleware>();

            app.UseCors("AllowAngularApp");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                db.Database.Migrate(); // optional: auto-applies pending migrations
                Implementation.Seeding.DbSeeder.Seed(db);
            }

            app.Run();
        }
    }
}
