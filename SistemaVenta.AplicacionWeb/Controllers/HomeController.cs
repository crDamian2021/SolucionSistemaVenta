using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Models;
using System.Diagnostics;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class HomeController : Controller
    {
        //LO CAMBIAMOS A PRIVADO SI TIRA ERROR
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

//AGREGAMOS UN METODO PARA LA VISTA DEL PERFIL Y TAMBIEN AGREGAMOS LA VISTA DE RAZOR
        public IActionResult Perfil()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
