using System.Collections.Generic;
using System.Data;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.TransactionsDates;

namespace AriesWebApi.Logic {
    public class FechaTransaccionCL {
        private FechaTransaccionDao _fechaDao { get; } = new FechaTransaccionDao ();
        public IEnumerable<FechaTransaccion> GetAll (string companyid) => _fechaDao.GetAll (companyid);
        public DataTable GetDataTable (string companyId) => _fechaDao.GetDataTable (companyId);
    }
}