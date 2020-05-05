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
    public class AccountsController : AuthControllerBase {
        private readonly CuentaCL _cuentaCL = new CuentaCL ();
        [HttpGet ("{companyid}")]
        public IActionResult Get (string companyid) => Ok (_cuentaCL.GetAll (companyid));
        // => Ok (_cuentaCL.GetAll (companyid).Select(x=> new {id = x.Id, nombre = x.Nombre}));



    }
}