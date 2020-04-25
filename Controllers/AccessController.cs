using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AriesWebApi.Controllers
{
    public class AccessController : ControllerBase
    {
        [HttpPost]
        [Route("access/")]
        public ActionResult Login([FromBody] string usuario){

            //Verificar si el usuario existe en la base de datos
            //Guardar el token en la base de datos
            //En el verify se verifica que el token que me esten
            //enviando sea el mismo que tengo guardado                         
            return Ok(usuario);
        }

    }
}