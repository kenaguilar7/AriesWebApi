using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Users;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class UsersController : AuthControllerBase {

        readonly UserCL _userCL = new UserCL ();

        [HttpGet]
        public IActionResult Get () => Ok (_userCL.GetAll ());

        [HttpGet ("{id}")]
        public IActionResult Get (int id) => Ok ();

        [HttpPost]
        public IActionResult Post ([FromBody] Usuario user) =>
            CreatedAtAction (nameof (Get), new { id = user.UsuarioId }, _userCL.Insert(user));

    }
}