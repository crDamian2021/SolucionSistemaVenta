using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Linq expressions nos ayudan a crear expresiones lambda pero que son dinámicas en runtime
using System.Linq.Expressions;

namespace SistemaVenta.BLL.Interfaces
{
    //ESTE ENTITY VA SER UNA CLASE 
    public interface IGenericRepository<TEntity> where TEntity:class 
    {
        //OBTENER
        Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro);

        //CREAR
        Task<TEntity> Crear(TEntity entidad);

        //EDITAR
        Task<bool> Editar(TEntity entidad);

        //ELIMINAR
        Task<bool> Eliminar(TEntity entidad);

        //CONSULTAR
        Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro);
        //        Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro =null);
    }
}
