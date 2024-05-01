using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AÑADIMOS LA RESFERENCIA QUE VAMOS A UTILIZRA
using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.DAL.Implementacion
{
    //ESTA CLASE SE VA HEREDAR DE IVENTAREPOSITORY
    //IMPLEMENTAOS LA INTERFAZ DE IVENTAREPOSITORY
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        //VAMOS A IMPLEMENTAR EL CONTEXTO DE NUESTRA BACE DE DATOS
        //ERROR EN EL CODIGO
        private readonly DBContext.LIBRERIA_CENTROContext _dbContext;
        private DetalleVenta listaResumen;

        //dbContex o dbContext
        public VentaRepository(DBContext.LIBRERIA_CENTROContext dbContex) : base(dbContex)
        {
            //_dbContext no es lo mismo que dbContex
            _dbContext = dbContex;
        }

        public async Task<Venta> Registra(Venta entidad)
        {
            //Venta VentaGenerada = new Venta();
            Venta VentaGenerada = new();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //ITERAR LOS PRODUCTOS DE VENTAS Y REDUCIRLOS SEGUN LO VENDIDO CMABIAR EL STOCK 
                    foreach (DetalleVenta dv in entidad.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        //producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        producto_encontrado.Stock -= dv.Cantidad;
                        _dbContext.Productos.Update(producto_encontrado);


                    }
                    await _dbContext.SaveChangesAsync();

                    //LA GESTION QUE SE VA ESTAR UTILIZANDO ES VENTA 
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "Venta").First();

                    //SE ACTUALIZARA LA FECHA Y TAMBIEN EL NUMERO CORELATIVO SE VA SUMAR 1
                    // ORIGIAL correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.UltimoNumero++;
                    correlativo.FechaActualizacion = DateTime.Now;

                    //SE ACTUALIZRA EL NUMERO CORRELATIVO
                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    await _dbContext.Venta.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    VentaGenerada = entidad;

                    transaction.Commit();


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;


                }
            }
            return VentaGenerada;
        }

        public async Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {

            List<DetalleVenta> listaResumen = await _dbContext.DetalleVenta
                        .Include(v => v.IdVentaNavigation)
                        .ThenInclude(u => u.IdUsuarioNavigation)
                        .Include(v => v.IdVentaNavigation)
                        .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                        .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date &&
                        dv.IdVentaNavigation.FechaRegistro.Value.Date <= FechaFin.Date)
                        .ToListAsync();


            return listaResumen;
        }






        /*public async Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {

                List<DetalleVenta> listaResumen = await _dbContext.DetalleVenta
                            .Include(v => v.IdVentaNavigation)
                            .ThenInclude(u => u.IdUsuarioNavigation)
                            .Include(v => v.IdVentaNavigation)
                            .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                            .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date &&
                            dv.IdVentaNavigation.FechaRegistro.Value.Date <= FechaFin.Date)
                            .ToListAsync();

           
                return listaResumen;
            


            
        }*/


    }
}