using ChauffeurApp.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ChauffeurApp.Application.MappingProfiles;
using ChauffeurApp.API.Extensions;
using ChauffeurApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Serilog;
using ChauffeurApp.Application.Services.IServices;
using ChauffeurApp.Application.Services;
using ChauffeurApp.Shared.Services.IServices;
using ChauffeurApp.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Log/ChauffeurLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IFileManager, FileManager>();
builder.Services.AddTransient<IClaimService,ClaimService>();


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{

    //seed roles
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationUserRole>>();

    var roles = new[]
    {
        "Admin",
        "Passenger",
        "Chauffeur"
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new ApplicationUserRole(role));
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
