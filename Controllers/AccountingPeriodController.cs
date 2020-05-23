using System;
using System.Collections.Generic;
using System.Linq;
using AriesWebApi.Entities.TransactionsDates;
using AriesWebApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers
{
    [Route ("api/companies/{companyid}/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class AccountingPeriodController : ControllerBase {
        private readonly FechaTransaccionCL _fechaTransaccionCL = new FechaTransaccionCL ();
        
        [HttpGet(Name = "Get")]
        public IActionResult Get (string companyid) => Ok (_fechaTransaccionCL.GetAll (companyid));
        

        [HttpGet ("GetDataTable")]
        public IActionResult GetDataTable (string companyid) {
            return Ok (_fechaTransaccionCL.GetDataTable (companyid));
        }

        [HttpGet ("GetAvailableMonths")]
        public IActionResult GetAvailableMonths (string companyid) {

            var fechaTransaccions = _fechaTransaccionCL.GetAll (companyid);
            //No hay meses abiertos
            if (fechaTransaccions.FirstOrDefault () == null) {
                return Ok (new List<FechaTransaccion> () {
                    new FechaTransaccion (
                        id: 0,
                        fecha: DateTime.Today,
                        cerrada: false
                    )
                });
            } else {
                return Ok (BuildNewFechaTransaccionList (fechaTransaccions));
            }

        }

        [HttpPost]
        public IActionResult Post (string companyid, [FromBody] FechaTransaccion fechaTransaccion) {
            fechaTransaccion.Id = 67; 
            return CreatedAtRoute (
                routeName: "Get",
                routeValues : new { id = fechaTransaccion.Id,companyid=companyid },
                value : fechaTransaccion);

        }

        [HttpPut(Name= "CloseMonth")]
        public IActionResult CloseMonth(string companyid, [FromBody] FechaTransaccion fechaTransaccion){

            return Ok(); 
        }

        private IEnumerable<FechaTransaccion> BuildNewFechaTransaccionList (IEnumerable<FechaTransaccion> fechaTransaccions) {
            var proximo = (from c in fechaTransaccions orderby c.Fecha descending select new FechaTransaccion (fecha: c.Fecha.AddMonths (1))).FirstOrDefault ();

            var ultimo = (from c in fechaTransaccions orderby c.Fecha ascending select new FechaTransaccion (fecha: c.Fecha.AddMonths (-1))).FirstOrDefault ();

            return new FechaTransaccion[] { ultimo, proximo };
        }

    }
}