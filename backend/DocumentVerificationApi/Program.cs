using DocumentVerificationApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer(); // Add API Explorer for Swagger
builder.Services.AddSwaggerGen(); // Add Swagger support

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS policy to allow requests from the Angular app running on http://localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Allow requests from Angular app
                   .AllowAnyMethod() // Allow all HTTP methods (GET, POST, etc.)
                   .AllowAnyHeader() // Allow all headers
                   .AllowCredentials(); // Allow credentials (if needed)
        });
});

var app = builder.Build();

// Seed data on application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    DataSeeder.Initialize(context); // Seed data
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI(); // Enable Swagger UI
}

app.UseHttpsRedirection();

// Use the CORS policy
app.UseCors("AllowAngularApp");

app.UseAuthorization();
app.MapControllers(); // Map controller routes

app.Run();