global using PancakeAuthBackend.Models;
global using PancakeAuthBackend.Data;
global using PancakeAuthBackend.DataTransferObjects;
global using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PancakeAuthBackend.Services;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using PancakeAuthBackend;

var builder = WebApplication.CreateBuilder(args);

//add local configs
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<ISchoolService, SchoolService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<ISampleDataSeeder, SampleDataSeeder>();
builder.Services.AddScoped<IAccountService, AccountService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();

var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("azure_sql_auth_store");

builder.Services.AddDbContext<BackendDataContext>((options) => {
    options.UseSqlServer(connectionString);
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});


builder.Services.ConfigureJWTAuthentication(Configuration);

builder.Services.AddCors(o => {
    o.AddPolicy("AllowAll", policyBuilder => {
        policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<BackendDataContext>();

  //  await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();

    await scope.SeedIdentity();
};

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
