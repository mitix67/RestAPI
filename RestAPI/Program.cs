using RestAPI;
using RestAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddTransient<IWeatherForecastServicee, WeatherForecastService>();
builder.Services.AddDbContext<RestAPI.Entities.RestaurandDbContext>();
builder.Services.AddScoped<RestaurantSeeder>();

builder.Services.AddControllers();

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
