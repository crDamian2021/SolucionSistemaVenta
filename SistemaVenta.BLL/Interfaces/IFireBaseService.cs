using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SistemaVenta.BLL.Interfaces
{
    public interface IFireBaseService
    {
        //CREAMOS LOS METODOS QUE ESTAMOS UTILIZANDO
        Task<string> SubirStorange(Stream StreamArchivo,string CarpetaDestino, string NombreArchivo);

        Task<bool> EliminarStorange(string CarpetaDestino, string NombreArchivo);

    }
}
