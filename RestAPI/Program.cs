using RestAPI;
using RestAPI.Services;
using RestAPI.Entities;
using RestAPI.Controllers;
using AutoMapper;
using NLog.Web;
using RestAPI.Middleware;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using RestAPI.Models;
using RestAPI.Models.Validators;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RestAPI.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog();
// Add services to the container.

var authenticationsettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationsettings);

builder.Services.AddSingleton(authenticationsettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationsettings.JwtIssuer,
        ValidAudience = authenticationsettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationsettings.JwtKey))
    };
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("HasNationality", b => b.RequireClaim("Nationality", "Polish", "Watican"));
    option.AddPolicy("AtLeast20", b => b.AddRequirements(new MinimumAgeRequirement(20)));
});
builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddDbContext<RestAPI.Entities.RestaurandDbContext>();
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddAutoMapper(typeof(RestaurantMappingPro).Assembly);
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<RestaurantSeeder>();
    seeder?.Seed();
    // Use the seeder here
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
