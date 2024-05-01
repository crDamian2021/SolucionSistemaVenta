namespace SistemaVenta.AplicacionWeb.Models.ViewModel
{
    public class VMDashBoard
    {
        public int? TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public string? TotalProductos { get; set; }
        public string? TotalCategorias { get; set; }

        public List<VMVentasSemana> VentasUltimaSemanas { get; set; }

        public List<VMProductosSemana> ProductosTopUltimaSemanas { get; set; }
    }
}
