//IMPORTA LAS CONFIGURACIONES DE AUTOMAPPER
using SistemaVenta.AplicacionWeb.Utilidades.Automapper;
//Hacemor referencia a IOC
using SistemaVenta.IOC;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Hcemos referencia a metodo que abiamos crado anteriormentes
builder.Services.InyectarDependencia(builder.Configuration);

//AGREGAMOS AUTOMMAPER A NUESTA WEB
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
