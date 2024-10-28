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
        var SqlConStr = builder.Configuration.GetConnectionString("SqlConStr");

        // Inyeccion del ConStr
        builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(SqlConStr));

        // Inyeccion del servicio
        builder.Services.AddScoped<TecnicoServices>();
        builder.Services.AddScoped<TipoTecnicoServices>();
        builder.Services.AddScoped<ClientesService>();
        builder.Services.AddScoped<TrabajosService>();
        builder.Services.AddScoped<PrioridadesService>();
		builder.Services.AddScoped<TrabajosDetalleService>();
        builder.Services.AddScoped<ArticulosService>();
        builder.Services.AddScoped<CotizacionesService>();








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
