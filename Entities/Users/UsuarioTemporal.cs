using System;
using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Users
{
    public class UsuarioTemporal
    {

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string CCopy { get; set; }
        public string Asunto { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime UltimoEnvio { get; set; }
        public EstadoEnvio EstadoEnvio { get; set; }

        public UsuarioTemporal(string nombre, string apellido, string correoElectronico, string ccopy, string asunto, string cuerpo, DateTime ultimoEnvio, EstadoEnvio estadoEnvio)
        {
            Nombre = nombre;
            Apellido = apellido;
            CorreoElectronico = correoElectronico;
            CCopy = ccopy;
            Asunto = asunto;
            Cuerpo = cuerpo;
            UltimoEnvio = ultimoEnvio;
            EstadoEnvio = estadoEnvio;
        }

        public UsuarioTemporal(string nombre, string apellido, string correoElectronico, string ccopy, string asunto, string titulo, string cuerpo)
        {
            Nombre = nombre;
            Apellido = apellido;
            CorreoElectronico = correoElectronico;
            Titulo = titulo; 
            CCopy = ccopy;
            Asunto = asunto;
            Cuerpo = cuerpo;
        }
    }
}
