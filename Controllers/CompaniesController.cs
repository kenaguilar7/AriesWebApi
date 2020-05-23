using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CompaniesController : AuthControllerBase {
        private readonly CompañiaCL _companyDao = new CompañiaCL ();

        [HttpGet]
        public IActionResult Get () => Ok (_companyDao.Get ());

        [HttpGet ("{id}", Name = "GetCompany")]
        public IActionResult Get (string id) {

            var companyToReturn = _companyDao.Get ().FirstOrDefault (uid => uid.Codigo.ToUpper () == id.ToUpper ());

            if (companyToReturn != null) {
                return Ok (companyToReturn);
            } else {
                return NotFound ();
            }
        }

        [HttpGet ("GetNewCode")]
        public IActionResult GetNewCode () => Ok (_companyDao.NuevoCodigo ());

        [HttpPost]
        public IActionResult Post ([FromBody] Compañia company) {

            var idcopyfrom = Request.Headers["copyfromid"].FirstOrDefault ();

            try {
                company = _companyDao.Insert (company, null, idcopyfrom);
            } catch (Exception ex) {
                return BadRequest (ex);
            }

            return CreatedAtRoute (
                routeName: "GetCompany",
                routeValues : new { id = company.Codigo },
                value : company);
        }

        [HttpPut ("{id}")]
        public IActionResult Put (long id, [FromBody] Compañia compania) {

            Usuario user = new Usuario ();

            try {
                _companyDao.Update (compania, user);
                return Ok ();
            } catch (System.Exception) {
                //todo log
                return NotFound ();
            }

        }
    }
}