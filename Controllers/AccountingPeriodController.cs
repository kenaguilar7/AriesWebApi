using System;
using System.Collections.Generic;
using System.Linq;
using AriesWebApi.Entities.TransactionsDates;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers
{
    [Route("api/companies/{companyid}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountingPeriodController : ControllerBase
    {
        private readonly FechaTransaccionCL _fechaTransaccionCL = new FechaTransaccionCL();

        [HttpGet(Name = "Get")]
        public IActionResult Get(string companyid) => Ok(_fechaTransaccionCL.GetAll(companyid));

        [HttpGet("GetDataTable")]
        public IActionResult GetDataTable(string companyid)
        {
            return Ok(_fechaTransaccionCL.GetDataTable(companyid));
        }

        [HttpGet("GetAvailableMonths")]
        public IActionResult GetAvailableMonths(string companyid)
        {
            return Ok(_fechaTransaccionCL.GetAvailablePostingPeriodsForBeCreated(companyid));
        }

        [HttpPost]
        public IActionResult Post(string companyid, [FromBody] FechaTransaccion fechaTransaccion)
        {
            var userId = 1;
            var newEntity = _fechaTransaccionCL.Insert(companyid, userId, fechaTransaccion);

            fechaTransaccion.Id = 67;
            return CreatedAtRoute(
                routeName: "Get",
                routeValues: new { id = newEntity.Id, companyid = companyid },
                value: newEntity);

        }

        //[HttpPut(Name = "CloseMonth")]
        //public IActionResult CloseMonth(string companyid, [FromBody] FechaTransaccion fechaTransaccion)
        //{

        //    return Ok();
        //}



    }
}