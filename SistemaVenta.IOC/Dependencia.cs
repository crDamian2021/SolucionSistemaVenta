using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//agreagar las dependencias
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SistemaVenta.DAL.Interfaces;
using SistemaVenta.DAL.Implementacion;

using SistemaVenta.BLL.Interfaces;
using SistemaVenta.BLL.Implementacion;




namespace SistemaVenta.IOC
{
    public static class Dependencia
    {
        //Creamos una referencia a lo que va hacer la cadena de coneccion
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            //adergamos el refrencia de la coneccion de dependencia de la libreria de contexto que esta en la capa apliacaion web
            services.AddDbContext<LIBRERIA_CENTROContext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });

            /*
             AddTransient es un método en ASP.NET Core utilizado para registrar servicios en el contenedor de inyección de dependencias. 
            Al usar AddTransient, se crea una nueva instancia del servicio cada vez que se solicita, lo que es útil para servicios sin estado.
             */
            //las inyecciones van a varaiar dentro de la clase igenrecrepositori
            services.AddTransient(typeof(DAL.Interfaces.IGenericRepository<>), typeof(GenericRepository<>));


            /*AddScoped es un método en ASP.NET Core utilizado para registrar servicios en el contenedor de inyección de dependencias.
             * Cuando se utiliza, el servicio se crea una vez por solicitud de cliente (HTTP), asegurando que se utilice la misma
             * instancia del servicio durante toda la duración de esa solicitud, pero se creará una nueva instancia para cada solicitud 
             * de cliente diferente. Esto es útil para servicios que necesitan mantener estado durante una solicitud HTTP, pero no necesitan
             * persistir entre solicitudes.*/
             services.AddScoped<IVentaRepository, VentaRepository>();

            //LOGICA DE ENVIO DE CORREOS ERROR 
             services.AddScoped<ICorreoService, CorreoService>();


            //LA LOGICA DE SERVICO DE ALMACENAMEIENTO

            services.AddScoped<IFireBaseService, FireBaseService>();


            //AGREGAMOS LA DEPENDENCIA PARA LOS UTLIDADES  
            services.AddScoped<IUtilidadesService, UtilidadesService>();

            //AGREGAMOS LA DEPENDENCIA PARA LOS ROLES 
            services.AddScoped<IRolService, RolService>();


            //AGREGAMOS LA DEPENDENCIA USUARIO
            services.AddScoped<IUsuarioService, UsuarioService>();

            //AGREGAMOS LA DEPENDENCIA CATEGORIA
            services.AddScoped<ICategoriaService, CategoriaService>();


        }
    }
}
