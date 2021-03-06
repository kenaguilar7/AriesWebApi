using System;
using System.Net.Mail;
using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Verifications
{
    public class VerificaString
    {
          /// <summary>
        /// Devuelve una string que puede ser utilizado como mascara 
        /// para los campos de identificacion
        /// </summary>
        /// <param name="tipoID"></param>
        /// <returns></returns>
        public static string MascaraIdentificacion(TipoID tipoID)
        {

            switch (tipoID)
            {
                case TipoID.CEDULA_JURIDICA: return "3-000-000000";
                case TipoID.CEDULA_NACIONAL: return "0-0000-0000";
                case TipoID.DIMEX: return "000000000000";
                case TipoID.NITE: return "0000000000";
                default: return "null";

            }

        }



        /// <summary>
        /// Retorna falso si el campo es nulo blanco o tiene espacios en blanco
        /// retorna true si el campo es valido
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="campo"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(string texto, string campo, out string mensaje)
        {

            if (string.IsNullOrWhiteSpace(texto))
            {
                mensaje = $"El campo {campo} no puede estar vacio";
                return false;
            }
            else
            {
                mensaje = "Campo validado correcto";
                return true;
            }

        }

        /// <summary>
        /// Retorna true si todo esta bien
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoID"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static bool VerificarID(string id, TipoID tipoID, out string mensaje)
        {

            var retorno = true;


            /**
             *   case TipoID.CEDULA_JURIDICA: return "3-000-000000";
                case TipoID.CEDULA_NACIONAL: return "0-0000-0000";
                case TipoID.DIMEX: return "000000000000";
                case TipoID.NITE: return "0000000000";
             */
            switch (tipoID)
            {
                case TipoID.CEDULA_JURIDICA:
                    if (id.Length != 12)
                    {
                        mensaje = "Formato de cédula incorrecto";
                        return false;
                    }
                    break;
                case TipoID.CEDULA_NACIONAL:
                    if (id.Length != 11)
                    {
                        mensaje = "Formato de cédula incorrecto";
                        return false;
                    }
                    break;
                case TipoID.DIMEX:
                    if (id.Length != 12)
                    {
                        mensaje = "Formato de cédula incorrecto";
                        return false;
                    }
                    break;
                case TipoID.NITE:
                    if (id.Length != 10)
                    {
                        mensaje = "Formato de cédula incorrecto";
                        return false;
                    }
                    break;
                default:
                    break;
            }

            mensaje = "Formato de cédula correctos";

            return retorno;
        }
        /// <summary>
        /// Retorna true si el correo es correcto
        /// o false si es incorrecto
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool ValidarEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return false;
                }
                else
                {
                    new MailAddress(email);
                    return true;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}