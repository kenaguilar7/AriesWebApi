using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Users;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class UsersController : ControllerBase {

        private readonly UserCL _userCL = new UserCL ();

        // [Authorize]
        [HttpGet]
        public IActionResult Get () => Ok (_userCL.GetAll ());

        // [Authorize]
        [HttpGet ("{id}",Name = "GetUser")]
        public IActionResult Get (int id) {

            var user = _userCL.GetAll ().FirstOrDefault (u => u.UsuarioId == id);
            if (user != null)
                return Ok (user);
            else
                return NotFound ();
        }

        [HttpPost]
        public IActionResult Post ([FromBody] Usuario user) {
            //Pendiente poner la ruta de creacion en el
            _userCL.Insert (user);
            // return NotFound();     
            return CreatedAtRoute (
                routeName: "GetUser",
                routeValues : new { id = user.UsuarioId },
                value : user);
        }

        [HttpPut ("{id}")]
        public IActionResult Update (int id, [FromBody] Usuario user) {

            /*Pasar esta verificion a la clase de verificaciones*/
            var userFromDb = _userCL.GetAll ().FirstOrDefault (u => u.UsuarioId == id);

            if (userFromDb == null) {
                return NotFound ();
            }
            //Hacer una verificacion aqui, en estos momentos este metodo devulve siempre nocontent 
            //cosa que no es correcta
            _userCL.Update (user);
            return Ok ();
        }

    }
}