using System.Linq;
using AriesWebApi.Entities.Entries;
using AriesWebApi.Entities.TransactionsDates;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers {
    // endpoints.MapControllerRoute ("entries", "bookentry/{bookentryid}/{controller=Home}/{action=Index}");
    [Route ("api/bookentry/{bookentryid}/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class BookTransactionController : ControllerBase {

        private readonly TransaccionCL _transaccionCL = new TransaccionCL ();

        [HttpPost]
        public IActionResult Post (int bookEntryId, [FromBody] Transaccion transaccion) {
            var userId = 1;
            var newTransaction = _transaccionCL.Insert (transaccion, bookEntryId, userId);
            
            return CreatedAtRoute (
                routeValues : new { id = newTransaction.Id },
                value : newTransaction);


        }

        [HttpGet]
        public IActionResult Get (int bookEntryId) {

            var lst = _transaccionCL.GetCompleto (bookEntryId);
            return Ok (lst);
        }

        [HttpPut ("{transactionid}")]
        public IActionResult Put (long bookEntryId, [FromBody] Transaccion transaccion) {
            var userId = 1;
            _transaccionCL.Update (transaccion, userId);
            return Ok ();
        }

        [HttpDelete ("{transactionid}")]
        public IActionResult Delete (long bookEntryId, long transactionid) {
            var userId = 1;
            _transaccionCL.Delete (transactionid, bookEntryId, userId);
            return Ok ();
        }

    }
}