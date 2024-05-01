using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{

    //SIEMPRE CAMBIAMOS LA CLASE A PUBLICA
    public interface IUtilidadesService  
    {
        //CREAMOS DOS METODOS
        string GenerarClave();

        string ConvertirSha256(string texto);
    }
}
