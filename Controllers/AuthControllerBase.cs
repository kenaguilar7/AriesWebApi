using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers
{
    public class AuthControllerBase : ControllerBase
    {

        public bool Verify(string token)
        {
            ///Ir a buscar en la base de datos si el token es el mismo
            //sino 
            return true; 
            
        }
        
    }
}