using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string? Codigo { get; set; }
        public string? Marca { get; set; }
        public string? Descrpcion { get; set; }
        public int? Stock { get; set; }
        public string? UrlImagen { get; set; }
        public string? NombreImgen { get; set; }
        public decimal? Precio { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaDeRegistro { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categoria? IdCategoriaNavigation { get; set; }
    }
}
