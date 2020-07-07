using System.Linq;
using AriesWebApi.Entities.Entries;
using AriesWebApi.Entities.TransactionsDates;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers
{
    [Route("api/company/{companyid}/accountingperiod/{accountingperiodid}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BookEntryController : ControllerBase
    {

        private readonly AsientoCL _asientoCL = new AsientoCL();

        [HttpGet]
        public IActionResult Get(string companyid, double accountingperiodid) 
            => Ok(_asientoCL.GetAll(accountingperiodid)); 

        [HttpGet("GetPreEntry")]
        public IActionResult GetPreEntry(string companyId, double accountingperiodid)
        {

            FechaTransaccionCL ft = new FechaTransaccionCL();
            var fecha = ft.GetAll(companyId).FirstOrDefault(x => x.Id == accountingperiodid);

            if (fecha is null)
            {
                return NotFound();
            }
            else
            {

                return Ok(_asientoCL.GetPreEntry(companyId, fecha));
            }
        }

        [HttpPost]
        public IActionResult Post(string companyid, double accountingperiodid, [FromBody] Asiento asiento)
        {

            var userId = 1;
            var newEntry = _asientoCL.Insert(companyid, accountingperiodid, userId, asiento);

            return CreatedAtRoute(
                // routeName: $"api/company/{companyid}/accountingperiod/{accountingperiodid}/BookEntry",
                routeValues: new { id = newEntry.Id },
                value: newEntry);

        }

        [HttpPut]
        public IActionResult Put(string companyid, double accountingperiodid, [FromBody] Asiento asiento)
        {
            var userId = 1;
            _asientoCL.Update(companyid, accountingperiodid, userId, asiento);
            return Ok();
        }


        [HttpDelete("{asientoid}")]
        public IActionResult Delete(string companyid, double accountingperiodid, double asientoid)
        {
            var userId = 1;
            _asientoCL.Delete(companyid, accountingperiodid, userId, asientoid);
            return Ok();
        }


    }
}