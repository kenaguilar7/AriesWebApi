using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.security;
using AriesWebApi.Entities.Users;

namespace AriesWebApi.Data.Daos
{
    class Guachi
    {

        public static bool Consultar(Usuario usuario, VentanaInfo ventanaInfo, CRUDName cRUDName)
        {
            if (usuario.TipoUsuario == TipoUsuario.Administrador)
            {
                return true; 
            }

            var retorno = false;
            foreach (var item in usuario.Modulos)
            {
                ///Va a buscar solo en los modulos que tenga permiso 
                if (item.TienePermiso)
                {
                    ///busca todas las ventanas de ese modulo y pregunta cual es igual a la que le acaban de pasar 
                    ///por parametro y si tiene permiso de hacer la accion que tambien fue pasada por parametro
                    foreach (var ventana in item.LstVentanas)
                    {
                        if (ventana.VentanaInfo == ventanaInfo && (ventana.FindCRUD(cRUDName).TienePermiso))
                        {
                            return true;
                            
                        }
                    }
                }
            }

            return retorno;
        }


    }
}
