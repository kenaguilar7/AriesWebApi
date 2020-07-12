using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using AriesWebApi.Entities.Accounts;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Users;
using AriesWebApi.Logic;
using AriesWebApi.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers
{

    [Route("api/company/{companyid}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly CuentaCL _cuentaCL = new CuentaCL();

        [HttpGet]
        public IActionResult Get(string companyid)
            => Ok(_cuentaCL.GetAll(companyid));

        [HttpGet("{accountId}", Name = "GetById")]
        public IActionResult Get(string companyid, double accountId)
            => Ok(_cuentaCL.GetAll(companyid).FirstOrDefault(x => x.Id == accountId));

        [HttpGet("GetFullBalanceWithDateRange/{accountId}")]
        public IActionResult GetFullBalanceWithDateRange(string companyid, double accountId, double fromAccountPeriodId, double toAccountPeriodId)
        {
            return Ok(_cuentaCL.CuentaConSaldos(companyid, accountId, fromAccountPeriodId, toAccountPeriodId));
        }

        [HttpPost]
        public IActionResult Post(string companyid, [FromBody] Cuenta cuenta)
        {
            var validator = new CuentaValidator();
            var result = validator.Validate(cuenta, ruleSet:"Insert");

            if (result.IsValid)
            {
                var userId = 1;//TODO 
                var newCuenta = _cuentaCL.Insert(companyid, cuenta, userId);

                return CreatedAtRoute(
                    routeName: "GetById",
                    routeValues: new { companyid = companyid.ToString(), accountId = cuenta.Id },
                    value: newCuenta
                );
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }

        [HttpPut("{accountid}")]
        public IActionResult Put(string companyid, double accountid, Cuenta cuenta)
        {
            var validator = new CuentaValidator();
            var result = validator.Validate(cuenta, ruleSet: "Update");

            if (result.IsValid)
            {
                var userId = 1;
                _cuentaCL.Update(companyid, cuenta, userId);
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }

        [HttpDelete("{accountid}")]
        public IActionResult Delete(string companyid, double accountid)
        {
            var userId = 1;
            _cuentaCL.Delete(companyid, accountid, userId);
            return Ok();
        }

    }
}