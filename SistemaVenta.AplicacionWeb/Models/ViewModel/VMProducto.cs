namespace SistemaVenta.AplicacionWeb.Models.ViewModel
{
    public class VMProducto
    {
        public int IdProducto { get; set; }
        public string? Codigo { get; set; }
        public string? Marca { get; set; }
        public string? Descrpcion { get; set; }
        public int? IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public int? Stock { get; set; }
        public string? UrlImagen { get; set; }
        public string? Precio { get; set; }
        public int? EsActivo { get; set; }
     
    }
}
