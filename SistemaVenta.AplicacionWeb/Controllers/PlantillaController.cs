using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Implementacion;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
    {
        public IActionResult EnviarClave(string correo, string clave)
        {
            //COMPARTIR LA VISTA CON ViewData
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";
            return View();
        }

        public IActionResult RestablecerClave(string clave)
        {
            ViewData["CLAVE"] = clave;
            return View();
        }
    }
}
