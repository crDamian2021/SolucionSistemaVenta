using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//AGREGAMOS TODAS LAS REFERENCIAS NECESARIAS
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace SistemaVenta.DAL.Implementacion
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class

    {
        /*
         En Entity Framework, el contexto de la base de datos es repr esentado por una clase que 
        hereda de DbContext.Esta clase es responsable de realizar el seguimiento de las entidades, 
        administrar la conexión con la base de datos y ejecutar operaciones CRUD (Crear, Leer, Actualizar, Eliminar)
        en la base de datos.*/
        //CREAMOS EL CONTEXTO DE LA BACE DE DATOS
        private readonly LIBRERIA_CENTROContext _dbContext;

        public GenericRepository(LIBRERIA_CENTROContext dbContext)
        {
            _dbContext = dbContext;
        }


        //OBTENER
        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entidad = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filtro).ConfigureAwait(false);
                return entidad;
                

            }
            catch {
                throw;
            }
          /*  try
            {
                // TEntity entidad = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filtro);
                // return entidad;
                TEntity entidad = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filtro);
                if (entidad == null)
                {
                    throw new System.InvalidOperationException();
                }
                return entidad;


            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la entidad.", ex);
            }*/
        }

        //CREAR
        public async Task<TEntity> Crear(TEntity entidad)
        {
            try {
                _dbContext.Set<TEntity>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch {
                throw;
            }
        }

        //EDITAR
        public async Task<bool> Editar(TEntity entidad)
        {
            /* try
             {
                 _dbContext.Set<TEntity>().Update(entidad);
                 await _dbContext.SaveChangesAsync();
                 return true;
             }
             catch
             {
                 throw;
             }*/

            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        //ELIMINAR
        public async Task<bool> Eliminar(TEntity entidad)
        {
            try
            {
                //SE UTILIZA REMUVE NO ELIMINAR
                _dbContext.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
            IQueryable<TEntity> queryEntidad = filtro == null ? _dbContext.Set<TEntity>() : _dbContext.Set<TEntity>().Where(filtro);
            return queryEntidad;
        }



        //CONSULTAR
        /*public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>>? filtro = null)
        {
            IQueryable<TEntity> queryEntidad = filtro == null ? _dbContext.Set<TEntity>():_dbContext.Set<TEntity>().Where(filtro);
             return queryEntidad;

        }*/




    }
}
