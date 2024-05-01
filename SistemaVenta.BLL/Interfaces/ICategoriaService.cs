//using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//AGREGAMOS LAS REFRENCIAS QUE VAMOS A ESTAR UTILIZANDO
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ICategoriaService
    {
        //CREAMOS TODOS LOS METODS
        Task<List<Categoria>> Lista();

        Task<Categoria> Crear(Categoria entidad);

        Task<Categoria> Editar(Categoria entidad);

        Task<bool> Eliminar(int idCategoria);


    }
}
