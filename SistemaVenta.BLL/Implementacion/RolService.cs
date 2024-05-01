using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AGREGAMOS LAS REFERENCIAS
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    //IMPLEMENTAMOS LA INTERFAS
    public class RolService : IRolService
    {
        private readonly DAL.Interfaces.IGenericRepository<Rol> _repositorio;

        public RolService(DAL.Interfaces.IGenericRepository<Rol> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<List<Rol>> Lista()
        {
            //NOS DEVOLVERA LA LISTA DE ROLES
            IQueryable<Rol> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
