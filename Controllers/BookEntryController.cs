using AriesWebApi.Entities.Entries;
using AriesWebApi.Entities.TransactionsDates;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers {
    [Route ("api/company/{companyid}/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class BookEntry : ControllerBase {

        private readonly AsientoCL _asientoCL = new AsientoCL ();

        [HttpGet ("{monthId}")]
        public IActionResult Get (string companyid, double monthId) {
            var trasn = new FechaTransaccion () { Id = monthId };
            var lst = _asientoCL.GetAll (companyid,trasn);
            return Ok (lst);
        }

        

    }
}