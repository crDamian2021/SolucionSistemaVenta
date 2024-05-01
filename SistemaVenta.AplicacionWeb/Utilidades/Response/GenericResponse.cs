using MessagePack.Resolvers;

namespace SistemaVenta.AplicacionWeb.Utilidades.Response
{
    /// <summary>
    /// AGREGAR RESPUESTA A LA PETICIONES 
    /// </summary>
    public class GenericResponse<TObject>
    {
        public bool Estado { get; set; }

        public string? Mensaje { get; set; }

       public TObject? Objeto { get; set; }

        public List<TObject>? ListaObjeto { get; set; }
    }
}
