using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SistemaVenta.Entity
{
    public partial class LIBRERIA_CENTROContext : DbContext
    {
        public LIBRERIA_CENTROContext()
        {
        }

        public LIBRERIA_CENTROContext(DbContextOptions<LIBRERIA_CENTROContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<Configuracion> Configuracios { get; set; } = null!;
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Negocio> Negocios { get; set; } = null!;
        public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolMenu> RolMenus { get; set; } = null!;
        public virtual DbSet<TipoDocumentoVenta> TipoDocumentoVenta { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
 /*          if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local); DataBase=LIBRERIA_CENTRO;Integrated Security=true");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__CATEGORI__4BD51FA5A97EC82F");

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.EsActivo).HasColumnName("ES_ACTIVO");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_REGISTRO")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CONFIGURACIO");

                entity.Property(e => e.Propiedad)
                    .HasMaxLength(50)
                    .HasColumnName("PROPIEDAD");

                entity.Property(e => e.Recurso)
                    .HasMaxLength(50)
                    .HasColumnName("RECURSO");

                entity.Property(e => e.Valor)
                    .HasMaxLength(60)
                    .HasColumnName("VALOR");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__DETALLE___49DABA0CB55428DF");

                entity.ToTable("DETALLE_VENTA");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("ID_DETALLE_VENTA");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.CategoriaPrdoucto)
                    .HasMaxLength(100)
                    .HasColumnName("CATEGORIA_PRDOUCTO");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(100)
                    .HasColumnName("DESCRIPCION_PRODUCTO");

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");

                entity.Property(e => e.MarcaProducto)
                    .HasMaxLength(100)
                    .HasColumnName("MARCA_PRODUCTO");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("PRECIO");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TOTAL");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DETALLE_V__ID_VE__5629CD9C");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__MENU__4728FC60533E9C5B");

                entity.ToTable("MENU");

                entity.Property(e => e.IdMenu).HasColumnName("ID_MENU");

                entity.Property(e => e.Controlador)
                    .HasMaxLength(30)
                    .HasColumnName("CONTROLADOR");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.EsActivo).HasColumnName("ES_ACTIVO");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_REGISTRO")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icono)
                    .HasMaxLength(30)
                    .HasColumnName("ICONO");

                entity.Property(e => e.IdMenuPadre).HasColumnName("ID_MENU_PADRE");

                entity.Property(e => e.PaginaAccion)
                    .HasMaxLength(30)
                    .HasColumnName("PAGINA_ACCION");

                entity.HasOne(d => d.IdMenuPadreNavigation)
                    .WithMany(p => p.InverseIdMenuPadreNavigation)
                    .HasForeignKey(d => d.IdMenuPadre)
                    .HasConstraintName("FK__MENU__ID_MENU_PA__36B12243");
            });

            modelBuilder.Entity<Negocio>(entity =>
            {
                entity.HasKey(e => e.IdNegocio)
                    .HasName("PK__NEGOCIO__D81B51AF3F5A0BA6");

                entity.ToTable("NEGOCIO");

                entity.Property(e => e.IdNegocio)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_NEGOCIO");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.NombreLogo)
                    .HasMaxLength(100)
                    .HasColumnName("NOMBRE_LOGO");

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(50)
                    .HasColumnName("NUMERO_DOCUMENTO");

                entity.Property(e => e.PorcentajeImpuestos)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("PORCENTAJE_IMPUESTOS");

                entity.Property(e => e.SimboloMoneda)
                    .HasMaxLength(5)
                    .HasColumnName("SIMBOLO_MONEDA");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.UrlLogo)
                    .HasMaxLength(500)
                    .HasColumnName("URL_LOGO");
            });

            modelBuilder.Entity<NumeroCorrelativo>(entity =>
            {
                entity.HasKey(e => e.IdNumeroCorrelativo)
                    .HasName("PK__NUMERO_C__5D06583C2492A20C");

                entity.ToTable("NUMERO_CORRELATIVO");

                entity.Property(e => e.IdNumeroCorrelativo).HasColumnName("ID_NUMERO_CORRELATIVO");

                entity.Property(e => e.CantidadDigitos).HasColumnName("CANTIDAD_DIGITOS");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.Gestion)
                    .HasMaxLength(100)
                    .HasColumnName("GESTION");

                entity.Property(e => e.UltimoNumero).HasColumnName("ULTIMO_NUMERO");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__PRODUCTO__88BD0357CC7850DA");

                entity.ToTable("PRODUCTO");

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("CODIGO");

                entity.Property(e => e.Descrpcion)
                    .HasMaxLength(50)
                    .HasColumnName("DESCRPCION");

                entity.Property(e => e.EsActivo).HasColumnName("ES_ACTIVO");

                entity.Property(e => e.FechaDeRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_DE_REGISTRO")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .HasColumnName("MARCA");

                entity.Property(e => e.NombreImgen)
                    .HasMaxLength(100)
                    .HasColumnName("NOMBRE_IMGEN");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("PRECIO");

                entity.Property(e => e.Stock).HasColumnName("STOCK");

                entity.Property(e => e.UrlImagen)
                    .HasMaxLength(500)
                    .HasColumnName("URL_IMAGEN");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__PRODUCTO__ID_CAT__49C3F6B7");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__ROL__203B0F68D7AAE6D9");

                entity.ToTable("ROL");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.EsActivo).HasColumnName("ES_ACTIVO");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_REGISTRO")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.HasKey(e => e.IdRolMenu)
                    .HasName("PK__ROL_MENU__94BF0F1C64C0AA55");

                entity.ToTable("ROL_MENU");

                entity.Property(e => e.IdRolMenu).HasColumnName("ID_ROL_MENU");

                entity.Property(e => e.EsActivo).HasColumnName("ES_ACTIVO");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_REGISTRO")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMeun).HasColumnName("ID_MEUN");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.HasOne(d => d.IdMeunNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdMeun)
                    .HasConstraintName("FK__ROL_MENU__ID_MEU__3E52440B");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__ROL_MENU__ID_ROL__3F466844");
            });

            modelBuilder.Entity<TipoDocumentoVenta>(entity =>
            {
                entity.HasKey(e => e.IdTipoDocumentoVenta)
                    .HasName("PK__TIPO_DOC__5A9FE12FC2299AAA");

                entity.ToTable("TIPO_DOCUMENTO_VENTA");

                entity.Property(e => e.IdTipoDocumentoVenta).HasColumnName("ID_TIPO_DOCUMENTO_VENTA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.EsActivo).HasColumnName("ES_ACTIVO");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_REGISTRO")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__91136B9011D09C99");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .HasColumnName("CLAVE");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .HasColumnName("CORREO");

                entity.Property(e => e.EsActivo).HasColumnName("ES_ACTIVO");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_REGISTRO")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.NombreFoto)
                    .HasMaxLength(50)
                    .HasColumnName("NOMBRE_FOTO");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.UrlFotos)
                    .HasMaxLength(500)
                    .HasColumnName("URL_FOTOS");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__USUARIO__ID_ROL__4316F928");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__VENTA__F3B6C1B417D0562B");

                entity.ToTable("VENTA");

                entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");

                entity.Property(e => e.DocumentoCliente)
                    .HasMaxLength(16)
                    .HasColumnName("DOCUMENTO_CLIENTE");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_REGISTRO")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdTipoDocumentoVenta).HasColumnName("ID_TIPO_DOCUMENTO_VENTA");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.ImpuestoTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("IMPUESTO_TOTAL");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(30)
                    .HasColumnName("NOMBRE_CLIENTE");

                entity.Property(e => e.NumeroVenta)
                    .HasMaxLength(6)
                    .HasColumnName("NUMERO_VENTA");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("SUB_TOTAL");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TOTAL");

                entity.HasOne(d => d.IdTipoDocumentoVentaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdTipoDocumentoVenta)
                    .HasConstraintName("FK__VENTA__ID_TIPO_D__52593CB8");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__VENTA__ID_USUARI__534D60F1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
