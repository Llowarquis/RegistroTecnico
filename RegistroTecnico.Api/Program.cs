using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tecnicos.Data.Context;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RegistroTecnicoApiContext")
	?? throw new InvalidOperationException("Connection string 'RegistroTecnicoApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Redirigir la raiz a la ruta de Swagger
app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
	app.UseSwagger();
	app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
