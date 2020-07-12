using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.TransactionsDates;
using System.Linq;
using AriesWebApi.Entities.TransactionsDates.TransactionDateList;
using AriesWebApi.Entities.Accounts;
using System;

namespace AriesWebApi.Logic
{
    public class FechaTransaccionCL
    {
        private FechaTransaccionDao FechaDao { get; } = new FechaTransaccionDao();

        public IEnumerable<FechaTransaccion> GetAll(string companyid) => FechaDao.GetAll(companyid);

        public FechaTransaccion Insert(string companyId, int userId, FechaTransaccion fechaTransaccion)
        {
            return FechaDao.Insert(companyId, userId, fechaTransaccion);
        }

        public DataTable GetDataTable(string companyId) => FechaDao.GetDataTable(companyId);

        public IEnumerable<FechaTransaccion> GetAvailablePostingPeriodsForBeCreated(string companyId)
        {
            var postingPeriods = GetAll(companyId);

            if (postingPeriods.Count() > 0)
            {
                return CreateAvailablePostingPeriodsForBeCreate(companyId, postingPeriods);
            }
            else
            {
                return CreatePreEntity(DateTime.Now);
            }
        }

        private IEnumerable<FechaTransaccion> CreateAvailablePostingPeriodsForBeCreate(string companyId, IEnumerable<FechaTransaccion> postingPeriods)
        {

            if (HasJournalEntries(companyId, postingPeriods))
            {
                return CreatePreEntity(postingPeriods.GetNewerDate().Fecha.AddMonths(1));
            }
            else
            {
                return CreatePreEntity(postingPeriods.GetOlderDate().Fecha.AddMonths(-1),
                              postingPeriods.GetNewerDate().Fecha.AddMonths(1));
            }
        }

        private bool HasJournalEntries(string companyId, IEnumerable<FechaTransaccion> postingPeriods)
        {
            CuentaCL _cuentaCL = new CuentaCL();
            var accounts = _cuentaCL.CuentaConSaldos(companyId,
                                                     postingPeriods.GetOlderDate().Id,
                                                     postingPeriods.GetNewerDate().Id);

            return accounts.Count(row => row.CuentaConMovientos()) > 0;
        }

        private IEnumerable<FechaTransaccion> CreatePreEntity(DateTime fromDatePeriod)
        {
            return new List<FechaTransaccion>() { FechaDao.CreatePreEntity(fromDatePeriod) };
        }

        private IEnumerable<FechaTransaccion> CreatePreEntity(DateTime fromDatePeriod, DateTime toDatePeriod)
        {
            return new List<FechaTransaccion>() { FechaDao.CreatePreEntity(fromDatePeriod), 
                                                  FechaDao.CreatePreEntity(toDatePeriod) };
        }

    }
}