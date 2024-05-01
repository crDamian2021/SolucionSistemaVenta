using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class VentaController : Controller
    {
        /// AGREGAMOS UNA VIZSTA RAZOR  A LA NUEVA VENTA
        public IActionResult NuevaVenta()
        {
            return View();
        }


        public IActionResult HistorialVenta()
        {
            return View();
        }
    }
}
