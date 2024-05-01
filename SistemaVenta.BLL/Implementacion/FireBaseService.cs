using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//IMPLEMENTAMOS LA REFRENCIA PARA USAR LA INTERFAZ DE FIREBASE
using SistemaVenta.BLL.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using SistemaVenta.Entity;
using SistemaVenta.DAL.Interfaces;

namespace SistemaVenta.BLL.Implementacion
{
    public class FireBaseService : IFireBaseService
    {

        /*Esto significa que el campo solo puede ser modificado dentro de la clase que lo declara
         * y no puede ser modificado una vez que se ha asignado un valor. Este modificador se 
         * utiliza para garantizar la inmutabilidad de ciertos datos dentro de una clase y para 
         * limitar su acceso a solo lectura desde fuera de la clase.*/

        private readonly DAL.Interfaces.IGenericRepository<Configuracion> _repositorio;

        public FireBaseService(DAL.Interfaces.IGenericRepository<Configuracion> repositorio)
        {
            _repositorio = repositorio;
        }



        public async Task<string> SubirStorange(Stream StreamArchivo, string CarpetaDestino, string NombreArchivo)
        {
            string URLImagen = "";

            try
            {
                IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FIRE_BASE_STORAGE"));

                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["API_KEY"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(Config["EMAIL"], Config["CLAVE"]);

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(

                    Config["RUTA"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true

                    })
                    //CHILD SIRVE PAR CREAR CAPETAS TAMBIEN PARA CREAR ARCHIVOS 
                    .Child(Config[CarpetaDestino])
                    .Child(NombreArchivo)
                    .PutAsync(StreamArchivo, cancellation.Token);
                URLImagen = await task;

            }
            catch
            {
                URLImagen = " ";
            }
            return URLImagen;
        }

        public async Task<bool> EliminarStorange(string CarpetaDestino, string NombreArchivo)
        {
            try
            {
                IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FIRE_BASE_STORAGE"));

                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["API_KEY"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(Config["EMAIL"], Config["CLAVE"]);

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(

                    Config["RUTA"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true

                    })
                    //CHILD SIRVE PAR CREAR CAPETAS TAMBIEN PARA CREAR ARCHIVOS 
                    .Child(Config[CarpetaDestino])
                    .Child(NombreArchivo)
                    .DeleteAsync();

                await task;

                return true;

            }
            catch
            {
                return false;
            }
        }



    }
}
