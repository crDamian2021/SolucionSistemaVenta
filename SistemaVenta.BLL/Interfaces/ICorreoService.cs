using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ICorreoService
    {
        //ESTE APARTADO VA ENVIAR LOS CORREOS PERSONALISADOS
        //CORREO DESTINO ASUNTO MENESAJE

        Task<bool> EnviarCorreo(string CorreoDestino, string Asunto, string Mensaje);
    }
}
