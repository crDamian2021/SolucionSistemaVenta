using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//AGREGAMOS LAS REFERENCIAS
using SistemaVenta.Entity;

namespace SistemaVenta.DAL.Interfaces
{
    
    //ESTA INTEFAZ VA EREDAR DE IGENERCREPOSITORY
    public interface IVentaRepository :IGenericRepository<Venta>
    {
        //ASIGANAMOS LAS TAREAS QUE VA A REALAIZAR ESTA FUNCION
        Task<Venta> Registra(Venta entidad);

        //EL SIGUIENTE METODOS ES UNA LISTA DE VENTA LAS LISTA SON DIFERENTES HAY QUE VERIFICAR BIEN 
        Task <List<DetalleVenta>> Reporte(DateTime FechaInicio ,DateTime FechaFin);
    
    }
   
}
