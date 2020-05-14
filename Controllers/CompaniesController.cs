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


        [HttpGet("GetNewCode")]
        public IActionResult GetNewCode() => Ok(_companyDao.NuevoCodigo());

        // GET api/companies
        [HttpGet]
        public IActionResult Get () => Ok (_companyDao.Get ());

        // GET api/companies/5
        [HttpGet ("{id}", Name = "GetCompany")]
        public IActionResult Get (string id) {

            var companyToReturn = _companyDao.Get ().FirstOrDefault(uid => uid.Codigo.ToUpper() == id.ToUpper());

            if (companyToReturn != null) {
                return Ok (companyToReturn);
            } else {
                return NotFound ();
            }

        }

        // POST api/companies
        [HttpPost]
        public IActionResult Post ([FromBody] Compañia value) {

            var idcopyfrom = Request.Headers["copyfromid"].FirstOrDefault();
            
            // _companyDao.Insert(value, null, copiarDe:null);

            if (value is PersonaFisica) {
                var ss = (PersonaFisica) value;
            } else if (value is PersonaJuridica) {
                var ss = (PersonaJuridica) value;
            }

            // return NotFound();     
            return CreatedAtRoute (
                routeName: "GetCompany",
                routeValues : new { id = value.Codigo },
                value : value);

        }

        [Consumes (MediaTypeNames.Application.Json)]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        // PUT api/companies/c001
        [HttpPut ("{id}")]
        public IActionResult Put (long id, [FromBody] Compañia compania) {

            Usuario user = new Usuario ();
            var ouputMessage = "";

            if (_companyDao.Update (compania, user, out ouputMessage)) {
                return Ok ();
            } else {
                return NotFound ();
            }
        }

        // DELETE api/companies/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }

    }
}