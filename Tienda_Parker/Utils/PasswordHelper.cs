using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Parker.Utils
{
    public class PasswordHelper
    {
        // Método para encriptar la contraseña utilizando SHA-256
        public static string EncriptarContraseña(string contraseña)
        {
            // Crear una instancia del algoritmo SHA-256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la cadena de contraseña a un array de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));

                // Crear un StringBuilder para construir la cadena hash
                StringBuilder builder = new StringBuilder();

                // Convertir cada byte a un string hexadecimal
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                // Retornar la contraseña encriptada en formato hexadecimal
                return builder.ToString();
            }
        }

        // Método para verificar si la contraseña ingresada coincide con la encriptada
        public static bool VerificarContraseña(string contraseñaIngresada, string contraseñaEncriptada)
        {
            // Encriptar la contraseña ingresada y compararla con la almacenada
            string hashIngresado = EncriptarContraseña(contraseñaIngresada);
            return hashIngresado.Equals(contraseñaEncriptada);
        }
    }
}
