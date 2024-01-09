using RestAPI;
using RestAPI.Services;
using RestAPI.Controllers;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RestAPI.Entities.RestaurandDbContext>();
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(RestaurantMappingPro).Assembly);
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<RestaurantSeeder>();
    seeder?.Seed();
    // Use the seeder here
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
