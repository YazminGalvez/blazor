
using blazor.Components;
using blazor.Components.Data;
using blazor.Components.Servicio;
using Microsoft.Data.Sqlite;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<ServicioControlador>();
builder.Services.AddSingleton<ServicioJuegos>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

String ruta = "mibase.db";

using var conexion = new SqliteConnection($"DataSource ={ruta}");
conexion.Open();
var comando = conexion.CreateCommand();

comando.CommandText = @"
    CREATE TABLE IF NOT EXISTS
    juego( identificador integer, nombre text, jugado integer);

    CREATE TABLE IF NOT EXISTS
    configuracion( clave TEXT PRIMARY KEY, valor TEXT);

    INSERT OR IGNORE INTO configuracion (clave, valor) VALUES ('MostrarSoloPendientes', 'False');

   
    INSERT OR IGNORE INTO configuracion (clave, valor) VALUES ('FiltroNombre', '');
";
comando.ExecuteNonQuery();
conexion.Close();
app.Run();