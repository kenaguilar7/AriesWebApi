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
    public class CompaniesController : AuthControllerBase {

        private readonly CompañiaCL _companyDao = new CompañiaCL ();
        // GET api/companies
        [HttpGet]
        public IActionResult Get () => Ok(_companyDao.Get()); 
        
        // GET api/companies/5
        [HttpGet ("{id}")]
        public ActionResult<string> Get (int id) {
            return $"value {Verify("dfdsf").ToString()}";
        }

        // POST api/companies
        [HttpPost]
        public void Post ([FromBody] Compañia value) {

            // _companyDao.Insert (value, null);

        }
        
        
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT api/companies/c001
        [HttpPut ("{id}")]
        public IActionResult Put (long id,[FromBody] Compañia compania) {

            Usuario user = new Usuario ();
            var ouputMessage = "";

            if (_companyDao.Update (compania, user, out ouputMessage)) {
                return Ok(); 
            } else {
                return NotFound ();
            }
        }
        

        // DELETE api/companies/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }

    }
}