﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//AGREGAMOS TODAS LAS REFERENCIAS
//CAPA DE NEGOCIOS
using SistemaVenta.BLL.Interfaces;
//CAPA DE DATOS
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DAL.Interfaces.IGenericRepository<Categoria> _repositorio;

        public CategoriaService(DAL.Interfaces.IGenericRepository<Categoria> repositorio)
        {
            _repositorio = repositorio;
        }




        //IMPLEMENTAMOS LAS INTERFACES
        public async Task<List<Categoria>> Lista()
        {
            IQueryable<Categoria> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Categoria> Crear(Categoria entidad)
        {
            try
            {
                Categoria categoria_creada = await _repositorio.Crear(entidad);
                if (categoria_creada.IdCategoria == 0)
                    throw new TaskCanceledException("No se pudo crear la categoria");

                return categoria_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Categoria> Editar(Categoria entidad)
        {

            try
            {
                Categoria categoria_encontrada = await _repositorio.Obtener(c => c.IdCategoria == entidad.IdCategoria);
                categoria_encontrada.Descripcion = entidad.Descripcion;
                categoria_encontrada.EsActivo = entidad.EsActivo;
                //ALMACEN LA RESPUESTA DE EDITAR
                bool respuesta = await _repositorio.Editar(categoria_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar categoria");

                return categoria_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idCategoria)
        {
            try
            {
                Categoria categoria_encontrada = await _repositorio.Obtener(c => c.IdCategoria == idCategoria);
                if (categoria_encontrada == null)
                    throw new TaskCanceledException("categoria no existe");
               
                bool respuesta = await _repositorio.Eliminar(categoria_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

    }
}
