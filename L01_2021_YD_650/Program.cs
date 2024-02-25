using Microsoft.EntityFrameworkCore;
using L01_2021_YD_650.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Inyeccion por dependencia del strinf de conexcion al contexto
builder.Services.AddDbContext<usuarioContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("blogDBDbConnection")
            )
        );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
