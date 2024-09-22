using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Components;
using RegistroTecnico.DAL;
using RegistroTecnico.Services;

namespace RegistroTecnico;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Obtencion del connection string
        var ConStr = builder.Configuration.GetConnectionString("ConStr");

        // Inyeccion del ConStr
        builder.Services.AddDbContext<Contexto>(o => o.UseSqlite(ConStr));

        // Inyeccion del servicio
        builder.Services.AddScoped<TecnicoServices>();
        builder.Services.AddScoped<TipoTecnicoServices>();
        builder.Services.AddScoped<ClientesService>();
        builder.Services.AddScoped<TrabajosService>();
        builder.Services.AddScoped<PrioridadesService>();




        // De aqui pa'rriba
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
