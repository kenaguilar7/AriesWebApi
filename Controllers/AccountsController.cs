using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AriesWebApi.Entities.Accounts;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Users;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers {

    [Route ("api/company/{companyid}/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class AccountsController : ControllerBase {
        private readonly CuentaCL _cuentaCL = new CuentaCL ();
        [HttpGet]
        public IActionResult Get (string companyid) => Ok (_cuentaCL.GetAll (companyid));
        
        [HttpPost]
        public IActionResult Post ([FromBody] Cuenta cuenta) {
            

            // Todo Save
            
            return CreatedAtRoute (
                routeName: "Get",
                routeValues : new { id = cuenta.Id },
                value : cuenta
            );
        }

        [HttpPut("{accountid}")]
        public IActionResult Put (string companyid, double accountid, [FromBody] Cuenta cuenta) {
            var userId = 1; 
            _cuentaCL.Update(companyid,cuenta,userId); 
            return Ok ();
        }
        [HttpDelete ("{accountid}")]
        public IActionResult Delete(string companyid, double accountid){
            var userId = 1; 
            _cuentaCL.Delete(companyid, accountid, userId); 
            return Ok(); 
        }

    }
}