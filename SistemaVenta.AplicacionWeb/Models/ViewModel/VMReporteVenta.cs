namespace SistemaVenta.AplicacionWeb.Models.ViewModel
{
    public class VMReporteVenta
    {

        public string? FechaRegistro { get; set; }
        public string? NumeroVenta { get; set; }
        public string? TipoDocumentoVenta { get; set; }
        public string? DocumentoCliente { get; set; }
        public string? NombreCliente { get; set; }
        public decimal? SubTotalVenta { get; set; }
        public decimal? ImpuestoTotalVenta { get; set; }
        public decimal? TotalVenta { get; set; }
        public decimal? Producto { get; set; }
        public decimal? Cantiad { get; set; }
        public decimal? Precio { get; set; }
        public int? Total { get; set; }
    }
}
