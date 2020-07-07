using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.TransactionsDates;
using System.Linq;

namespace AriesWebApi.Logic
{
    public class FechaTransaccionCL
    {
        private FechaTransaccionDao FechaDao { get; } = new FechaTransaccionDao();

        public IEnumerable<FechaTransaccion> GetAll(string companyid) => FechaDao.GetAll(companyid);

        public DataTable GetDataTable(string companyId) => FechaDao.GetDataTable(companyId);

        public FechaTransaccion Insert(string companyId, int userId, FechaTransaccion fechaTransaccion)
        {
            return FechaDao.Insert(companyId, userId, fechaTransaccion);
        }

        public IEnumerable<FechaTransaccion> BuildNewFechaTransaccionList(string companyId, IEnumerable<FechaTransaccion> fechaTransaccions)
        {
            var _cuentaCL = new CuentaCL();
            //var ultimaFecha = (from c in fechaTransaccions orderby c.Fecha ascending select c).FirstOrDefault();
            fechaTransaccions.OrderBy(x => x.Fecha);

            var ultimaFecha = fechaTransaccions.LastOrDefault();
            var primerFecha = fechaTransaccions.FirstOrDefault();
            var lst = _cuentaCL.CuentaConSaldos(companyId, primerFecha.Id, ultimaFecha.Id);
            var hasSaldo = lst.FirstOrDefault(cuenta => cuenta.CuentaConMovientos());

            if (hasSaldo == null)
            {
                var proximo = (from c in fechaTransaccions orderby c.Fecha descending select new FechaTransaccion(fecha: c.Fecha.AddMonths(1))).FirstOrDefault();
                var ultimo = (from c in fechaTransaccions orderby c.Fecha ascending select new FechaTransaccion(fecha: c.Fecha.AddMonths(-1))).FirstOrDefault();
                return new FechaTransaccion[] { ultimo, proximo };
            }
            else
            {
                return new FechaTransaccion[] { (from c in fechaTransaccions orderby c.Fecha descending select new FechaTransaccion(fecha: c.Fecha.AddMonths(1))).FirstOrDefault() };
            }
        }
    }
}