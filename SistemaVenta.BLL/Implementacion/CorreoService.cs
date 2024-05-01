using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AGREGAMOS LAS REFERNCIAS AL SERVIDOR PARA ENVIAR LOS CORREOS
using System.Net;
using System.Net.Mail;
//AGREAMOS NUESTRAS CAPAS 
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    //CREAMOS UNA CALSE PUBLICA QUE VA A EREDAR DE LA CLASE ICORREOSERVICE
    public class CorreoService : ICorreoService

    {
        
        //CREAMOS NUESTRO CONTEXTO
        private readonly DAL.Interfaces.IGenericRepository<Configuracion> _repositorio;

        public CorreoService(DAL.Interfaces.IGenericRepository<Configuracion> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> EnviarCorreo(string CorreoDestino, string Asunto, string Mensaje)
        {
            try
            {
                IQueryable<Configuracion> query = await _repositorio.Consultar(c =>c.Recurso.Equals("SERVICIO_CORREO"));

                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                //AGREGAMOS NUESTRAS CREDENCIALES LA QUE ABIAMOS INCERTADO EN LA BACE DE DATOS
                var credenciales = new NetworkCredential(Config["CORREO"], Config["CLAVE"]);

                //CORREO DEL MENSAJE
                var correo = new MailMessage()
                {
                    From = new MailAddress(Config["CORREO"], Config["ALIAS"]),
                    Subject = Asunto,
                    Body =Mensaje,
                    IsBodyHtml= true
                };

                //A QUEINE SE LE ENVIARA EL CORREO
                correo.To.Add(new MailAddress(CorreoDestino));

                var clienteServidor = new SmtpClient()
                {
                    Host = Config["HOST"],
                    Port =  int.Parse(Config["PUERTO"]),
                    Credentials = credenciales,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true
                };

                //CORREO DEL ORIGEN
                clienteServidor.Send(correo);
                return true;


      
            }
            catch
            {
                return false;
            }
        }
    }
}
