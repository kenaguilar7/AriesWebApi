using System;
using System.Linq;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Users;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers {

    [Route ("api/companies")]
    [ApiController]
    [Produces ("application/json")]
    public class CompaniesController : ControllerBase {
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

        [HttpPost (Name = "Post")]
        public IActionResult Post ([FromBody] Compañia company) {

            var idcopyfrom = Request.Headers["copyfromid"].FirstOrDefault ();
            Usuario user = new Usuario (){UsuarioId=1};
            try {
                company = _companyDao.Insert (company, user, idcopyfrom);
            } catch (Exception ex) {
                return BadRequest (ex);
            }

            return CreatedAtRoute (
                routeName: "GetCompany",
                routeValues : new { id = company.Codigo },
                value : company);
        }

        [HttpPut (Name = "Put")]
        public IActionResult Put ([FromBody] Compañia compania) {

            Usuario user = new Usuario (){UsuarioId=1};

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