using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AGREAGAMOS LAS REFERENCIAS 
//SECURITY CRYPTOGRAPHY investigar

using SistemaVenta.BLL.Interfaces;
using System.Security.Cryptography;

namespace SistemaVenta.BLL.Implementacion
{
    //IMPLEMENTAMOS LA INTERFAZ 
    public class UtilidadesService : IUtilidadesService
    {
        public string GenerarClave()
        {
            //RETORNA UNA CADENA DE TEXTO ALEATORIA
            // N NUMEROS Y LETRAS 
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }
        public string ConvertirSha256(string texto)
        {
            /*es una clase en Java que se utiliza para construir cadenas de caracteres de manera eficiente. Sirve como una alternativa a
             * concatenar cadenas de caracteres usando el operador +. La principal ventaja de StringBuilder es que permite modificar cadenas 
             * de caracteres de manera eficiente, especialmente cuando se realizan muchas operaciones de concatenación.*/
            StringBuilder sb = new StringBuilder();


            //METODO PARA ENCRIPTAR LA CLAVE DEL USUARIO
            //SHA256MManaged
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;

                //CREAMOS UN ARAY PARA QUE LA CADENA QUE RECIBA LO PASE A BYTE
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();

        }

       
    }
}
