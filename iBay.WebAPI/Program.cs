using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using iBay.Entities.Contexts;
using Microsoft.OpenApi.Models;
using System.Reflection;
using iBay.Entities.Models;
using iBay.Entities.Repositories;
using iBay.WebAPI.Interfaces;
using iBay.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddDbContext<iBayContext>(options =>
{
    options
        .UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration.GetConnectionString("iBayContext"));
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policyBuilder =>
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "iBay API",
        Description = "API for iBay Ltd - Online Store Experience",
        Contact = new OpenApiContact
        {
            Name = "Marwen Meddeb , Nicolas Brun",
        }
    });
});

// AddScoped for the Models layer
builder.Services.AddScoped<IBasicRepository<User>, UserRepository>();
builder.Services.AddScoped<IBasicRepository<CartItem>, CartItemRepository>();
builder.Services.AddScoped<IBasicRepository<Product>, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
