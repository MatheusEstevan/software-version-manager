using Microsoft.EntityFrameworkCore;
using SoftwareVersionManager.Data.Context;
using SoftwareVersionManager.Data.Repositories;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Domain.Services;
using SoftwareVersionManager.Entities.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<Software>, SoftwareRepository>();
builder.Services.AddScoped<IService<Software>, SoftwareService>();
builder.Services.AddScoped<IRepository<SoftwareVersion>, SoftwareVersionRepository>();
builder.Services.AddScoped<IService<SoftwareVersion>, SoftwareVersionService>();
builder.Services.AddDbContext<SoftwareVersionManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SoftwareVersionManagerDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
