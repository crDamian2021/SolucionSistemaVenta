using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IUsuarioService
    {
        //CREAMOS LOS METODOS QUE SE VAN A IMPLEMENTAR CON LOS USUARIOS
        //PAUSAR Y RETORNAR
        Task<List<Usuario>> Lista();
        //DECLARAMOS QUE HACEPTE VALORES NUL
        Task<Usuario> Crear(Usuario entidad, Stream? Foto = null, string NombreFoto = "", string UrlPlantillaCorreo = "");
        //DECLARAMOS QUE HACEPTE VALORES NUL
        Task<Usuario> Editar(Usuario entidad, Stream? Foto = null, string NombreFoto = "");

        Task<bool> Eliminar(int IdUsuario);

        Task<Usuario> ObtenerPorCredenciales(string correo, string clave);

        Task<Usuario> ObtenerPorId(int IdUsuario);

        Task<bool> GuardarPerfil(Usuario entidad);

        Task<bool> CambiarClave(int IdUsuario, String ClaveActual, string ClaveNueva);

        Task<bool> RestablecerClave(string Correo, string UrlPlantillaCorreo);




    }
}
