//AGREGAMOS LAS REFERENCIAS DE LOS MODELOS 
//ENTRE OTROS

using SistemaVenta.AplicacionWeb.Models.ViewModel;
using SistemaVenta.Entity;
using System.Globalization;
using AutoMapper;


namespace SistemaVenta.AplicacionWeb.Utilidades.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()

        {
            #region Rol
            //MAPEO DE ORIGEN A DESTINO ROL
            CreateMap<Rol, VMRol>().ReverseMap();
            #endregion

            #region Usuario
            //MAPEO DE ORIGEN A DESTINO USUARIO
            CreateMap<Usuario, VMUsuario>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                destino.NombreRol,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Descripcion)
                );
            #endregion

            #region VMUsuario
            //LA LOGICA PUEDESER DIFERENTE
            CreateMap<VMUsuario, Usuario>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore());
            #endregion

            #region Negocio
            //MAPEO PARA NEGOCIO 
            CreateMap<Negocio, VMNegocio>()
                .ForMember(destino =>
                destino.PorcentajeImpuestos,
                opt => opt.MapFrom(origen => Convert.ToString(origen.PorcentajeImpuestos.Value,new CultureInfo("es-GT"))));

            CreateMap< VMNegocio ,Negocio > ()
               .ForMember(destino =>
               destino.PorcentajeImpuestos,
               opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PorcentajeImpuestos, new CultureInfo("es-GT"))));
            #endregion

            #region Categoria
            CreateMap<Categoria, VMCategoria>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<VMCategoria , Categoria>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1? true : false));

            #endregion

            #region Producto
            CreateMap<Producto, VMProducto>()
                 .ForMember(destino =>
                 destino.EsActivo,
                 opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                 )
                 .ForMember(destino =>
                 destino.NombreCategoria,
                 opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Descripcion)
                 )
                 .ForMember(destino =>
                 destino.Precio,
                 opt => opt.MapFrom(origen =>Convert.ToString(origen.Precio.Value, new CultureInfo("es-GT")))
                 );

            CreateMap<VMProducto, Producto>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                destino.IdCategoriaNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-GT")))
                );
            #endregion

            #region Tipo DocumentoVenta
            //MAPEO DE ORIGEN A DESTINO ROL
            CreateMap<TipoDocumentoVenta, VMTipoDocumentoVenta>().ReverseMap();
            #endregion

            #region Venta
            CreateMap<Venta, VMVenta>()
                .ForMember(destino =>
                destino.TipoDocumentoVenta,
                opt => opt.MapFrom(origen => origen.IdTipoDocumentoVentaNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.Usuario,
                opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.SubTotal,
                opt => opt.MapFrom(origen => Convert.ToString(origen.SubTotal.Value, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.ImpuestoTotal,
                opt => opt.MapFrom(origen => Convert.ToString(origen.ImpuestoTotal.Value, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/YYYY"))
                );
            //LO CONTRARIO
            CreateMap<VMVenta,Venta>()
                .ForMember(destino =>
                destino.SubTotal,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.SubTotal, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.ImpuestoTotal,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImpuestoTotal, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-GT")))
                );
            #endregion

            #region DetalleVenta
            CreateMap<DetalleVenta, VMDetalleVenta>()
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-GT")))           
                );
            //INVERTIR LA LOGICA
            CreateMap<VMDetalleVenta , DetalleVenta >()
               .ForMember(destino =>
               destino.Precio,
               opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-GT")))
               )
               .ForMember(destino =>
               destino.Total,
               opt => opt.MapFrom(origen => Convert.ToString(origen.Total, new CultureInfo("es-GT")))
               );



            //DETALLE DE VENTA
            CreateMap<DetalleVenta, VMReporteVenta>()
                .ForMember(destino =>
               destino.FechaRegistro,
               opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
               )
                .ForMember(destino =>
               destino.NumeroVenta,
               opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroVenta)
               )
                .ForMember(destino =>
                destino.TipoDocumentoVenta,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdTipoDocumentoVentaNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.DocumentoCliente,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.DocumentoCliente)
                )
                .ForMember(destino =>
                destino.NombreCliente,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.NombreCliente)
                )

                .ForMember(destino =>
                destino.SubTotalVenta,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.SubTotal.Value, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.ImpuestoTotalVenta,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.ImpuestoTotal.Value, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.TotalVenta,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                destino.Producto,
                opt => opt.MapFrom(origen => origen.DescripcionProducto)
                )//PRECIO
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-GT")))
                )
                .ForMember(destino =>
                 destino.Total,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-GT")))
                 );
            #endregion

            #region Menu
            CreateMap<Menu, VMMenu>()
                .ForMember(destino =>
                destino.SubMenus,
                opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation)
                );

            #endregion









        }
    }
}
