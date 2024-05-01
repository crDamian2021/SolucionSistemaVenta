using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AGREGAMOS LAS REFERENCIAS
using Microsoft.EntityFrameworkCore;
using System.Net;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System.Diagnostics.SymbolStore;

namespace SistemaVenta.BLL.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DAL.Interfaces.IGenericRepository<Usuario> _repositorio;
        private readonly IFireBaseService _fireBaseService;
        private readonly IUtilidadesService _utilidadesService;
        private readonly ICorreoService _correoService;

        public UsuarioService(
         DAL.Interfaces.IGenericRepository<Usuario> repositorio,
         IFireBaseService fireBaseService,
         IUtilidadesService utilidadesService,
         ICorreoService correoService
          )
            {
            _repositorio = repositorio;
            _fireBaseService = fireBaseService;
            _utilidadesService = utilidadesService;
            _correoService = correoService;

            }


        public async Task<List<Usuario>> Lista()
        {
            //VA INCULUIR EL USUARIO Y ROL DE NAVEGACION
            IQueryable<Usuario> query = await _repositorio.Consultar();
            //LA VARIABLE R VA A TENER ACCESO A USUARIO Y ROL DE NAVEGACION
            return query.Include(r => r.IdRolNavigation).ToList();
        }

        public async Task<Usuario> Crear(Usuario entidad, Stream? Foto = null, string NombreFoto = "", string UrlPlantillaCorreo = "")
        {
            //CREAMOS UNA VARIABLE QUE CUMPLA ESTA FUNCION
            Usuario usuario_existe = await _repositorio.Obtener(u => u.Correo == entidad.Correo);

            //POSIBLE ERROR
            if (usuario_existe != null)
            throw new TaskCanceledException("El correo existe");
            
            try
            {
                //GENRAMOS UNA CLAVE PARA EL USUARIO
                string clave_generada = _utilidadesService.GenerarClave();
                //ENCIPTAMOS LA CLAVE PARA EL USUARIO
                entidad.Clave = _utilidadesService.ConvertirSha256(clave_generada);
                entidad.NombreFoto = NombreFoto;

                //SI EL USUARIO NO EXISTE SE PODRA CREAR Y SUBIR LA FOTO Y ACCEDAR A FIREBASE STORAGE
                if (Foto != null)
                {
                    //DEVUELVE UN URL PARA QUE PODAMOS TENER ACCESO A LA IMAGNE
                    String urlFoto = await _fireBaseService.SubirStorange(Foto, "CARPETA_USUARIO", NombreFoto);
                    //TODA LA FUNCIO DE ARRIBA SE ALMACENARA EN LA VARIABLE DE ABAJO
                    entidad.UrlFotos = urlFoto;
                }


                /*CREAMOS OTRO USUARIO */
                Usuario usuario_creado = await _repositorio.Crear(entidad);

                //SI EL USUARIO EXISTE
                if (usuario_creado.IdUsuario == 0)
                    throw new TaskCanceledException("No encontrado");

                if(UrlPlantillaCorreo != "")
                {
                    UrlPlantillaCorreo = UrlPlantillaCorreo.Replace("[CORREO]", usuario_creado.Correo).Replace("[CLAVE]", clave_generada);


                    string htmlCorreo = "";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                   //OBTENEMOS LAS SOLICITUDE DE LA PETICON  DE HTTPWEBREQUEST
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            //LE ESTAMOS DANOD LECTURA A ESTA PAGINA PERO CON UNA CODIFICACION CORRECTA QUE TIENE LA PROPIA PAGINA.
                            StreamReader readerStream = null;

                            if (response.CharacterSet == null)
                                readerStream = new StreamReader(dataStream);
                            else
                                readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                            //ReadToEnd(); LEER HASTA EL FINAL
                            htmlCorreo = readerStream.ReadToEnd();
                            //LIBERAMOS TODOS LOS RECURSO
                            response.Close();
                            readerStream.Close();


                        }
                    }


                    if (htmlCorreo != "")
                        await _correoService.EnviarCorreo(usuario_creado.Correo, "Cuenta creada", htmlCorreo);
                }
                //OBTENER ESTE USUSARIO CREADO Y CONUSLTAR USUSARIO CREADO 
                IQueryable<Usuario> query = await _repositorio.Consultar(u => u.IdUsuario == usuario_creado.IdUsuario);
                usuario_creado = query.Include(r => r.IdRolNavigation).First();

                return usuario_creado;



            }
            catch(Exception Ex)
            {
                throw;
            }
        }

        public async Task<Usuario> Editar(Usuario entidad, Stream? Foto = null, string NombreFoto = "")
        {
            //CREAMOS UNA VARIABLE QUE CUMPLA ESTA FUNCION
            //EDITAMOS EL CORREO PERO NO AL QUE ESTAMOS TRABAJANOD COMO ADMIN
            Usuario usuario_existe = await _repositorio.Obtener(u => u.Correo == entidad.Correo && u.IdUsuario != entidad.IdUsuario);

            //POSIBLE ERROR
            if (usuario_existe != null)
                throw new TaskCanceledException("El correo existe");


            try
            {
                //CREAMOS UNA CONSULTA PARA CONSULATAR EL USUARIO
                IQueryable<Usuario> queryUsusario = await _repositorio.Consultar(u => u.IdUsuario == entidad.IdUsuario);

                //OBTENOMOS EL PRIMER USUSARIO
                Usuario usuario_editar = queryUsusario.First();
                usuario_editar.Nombre = entidad.Nombre;
                usuario_editar.Correo = entidad.Correo;
                usuario_editar.Telefono =entidad.Telefono;
                usuario_editar.IdRol = entidad.IdRol;

                if (usuario_editar.NombreFoto == "")
                    usuario_editar.NombreFoto = NombreFoto;
               
                if(Foto != null)
                {
                    string urlFoto = await _fireBaseService.SubirStorange(Foto, "CARPETA_USUARIO", usuario_editar.NombreFoto);
                    usuario_editar.UrlFotos = urlFoto;
                }
                bool respuesta = await _repositorio.Editar(usuario_editar);

                    if(!respuesta)
                    throw new TaskCanceledException("No encontrado");

                    //ESCOGERA EL PRIMER USUSARIO
                Usuario usuario_editado = queryUsusario.Include(r => r.IdRolNavigation).First();

                return usuario_editado;


            }
            catch
            {
                throw;

            }

        }

        public async Task<bool> Eliminar(int IdUsuario)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == IdUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                string nombreFoto = usuario_encontrado.NombreFoto;
                bool respuesta = await _repositorio.Eliminar(usuario_encontrado);

                if (respuesta)
                    await _fireBaseService.EliminarStorange("CARPETA_USUARIO",nombreFoto);

                return true;


            }
            catch
            {
                throw;

            }
        }

        public async Task<Usuario> ObtenerPorCredenciales(string correo, string clave)
        {
            string clave_encriptada = _utilidadesService.ConvertirSha256(clave);

            Usuario usuario_encontrado = await _repositorio.Obtener(u => u.Correo.Equals(correo) && u.Clave.Equals(clave_encriptada));

            return usuario_encontrado;

        }

        public async Task<Usuario> ObtenerPorId(int IdUsuario)
        {
            IQueryable<Usuario> query = await _repositorio.Consultar(u => u.IdUsuario == IdUsuario);
            Usuario resultado = query.Include(r => r.IdRolNavigation).FirstOrDefault();

            return resultado;
        }

        public async Task<bool> GuardarPerfil(Usuario entidad)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == entidad.IdUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                usuario_encontrado.Correo = entidad.Correo;
                usuario_encontrado.Telefono = entidad.Telefono;

                bool respuesta = await _repositorio.Editar(usuario_encontrado);
                return respuesta;

            }
            catch
            {
                throw;

            }
        }

        public async Task<bool> CambiarClave(int IdUsuario, string ClaveActual, string ClaveNueva)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == IdUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                if(usuario_encontrado.Clave != _utilidadesService.ConvertirSha256(ClaveActual))
                    throw new TaskCanceledException("Contrasenia incorrecta");

                usuario_encontrado.Clave = _utilidadesService.ConvertirSha256(ClaveNueva);

                bool respuesta = await _repositorio.Editar(usuario_encontrado);

                return respuesta;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> RestablecerClave(string Correo, string UrlPlantillaCorreo)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.Correo == Correo);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("No encontramos ningun usuario");

                string clave_generada = _utilidadesService.GenerarClave();
                usuario_encontrado.Clave = _utilidadesService.ConvertirSha256(clave_generada);



                UrlPlantillaCorreo = UrlPlantillaCorreo.Replace("[CLAVE]",clave_generada);


                string htmlCorreo = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                //OBTENEMOS LAS SOLICITUDE DE LA PETICON  DE HTTPWEBREQUEST
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        //LE ESTAMOS DANOD LECTURA A ESTA PAGINA PERO CON UNA CODIFICACION CORRECTA QUE TIENE LA PROPIA PAGINA.
                        StreamReader readerStream = null;

                        if (response.CharacterSet == null)
                            readerStream = new StreamReader(dataStream);
                        else
                            readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                        //ReadToEnd(); LEER HASTA EL FINAL
                        htmlCorreo = readerStream.ReadToEnd();
                        //LIBERAMOS TODOS LOS RECURSO
                        response.Close();
                        readerStream.Close();


                    }
                }

                bool correo_enviado = false;

                if (htmlCorreo != "")
                    correo_enviado= await _correoService.EnviarCorreo(Correo, "contrasenia restablecida", htmlCorreo);

                if (!correo_enviado)
                    throw new TaskCanceledException("Tenemos problemas intentalo de nueevo mas tarde");

                bool respuesta = await _repositorio.Editar(usuario_encontrado);

                return respuesta;



            }

            catch
            {
                throw;
            }
        }
    }
}
