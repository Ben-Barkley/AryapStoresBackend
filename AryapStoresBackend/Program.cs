using Aryap.Core;
using Aryap.Core.Services;
using Aryap.Core.Services.Interfaces;
using Aryap.Data.Entities;
using Aryap.Shared.Repositories.Implementation;
using Aryap.Shared.Repositories.Interface;
using Aryap.Shared.UnitOfWork;
using Core.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ensure correct DbContext is injected where DbContext is required
builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DbDatabaseContext>());

// Register services
builder.Services.AddScoped<IClothesService, ClothesService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Register generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register AutoMapper (uncomment and ensure MappingProfile is defined)
//builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClothesStoreAPI", Version = "v1" });
});


// Add Logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

// Build the app
var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClothesStoreAPI v1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
