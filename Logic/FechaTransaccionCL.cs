using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.TransactionsDates;
using System.Linq; 

namespace AriesWebApi.Logic {
    public class FechaTransaccionCL {
        private FechaTransaccionDao FechaDao { get; } = new FechaTransaccionDao ();
        private CuentaCL CuentaCL = new CuentaCL(); 

        public IEnumerable<FechaTransaccion> GetAll (string companyid) => FechaDao.GetAll (companyid);
        public DataTable GetDataTable (string companyId) => FechaDao.GetDataTable (companyId);
        public IEnumerable<FechaTransaccion> BuildNewFechaTransaccionList(string companyId, IEnumerable<FechaTransaccion> fechaTransaccions)
        {
            //var ultimaFecha = (from c in fechaTransaccions orderby c.Fecha ascending select c).FirstOrDefault();
            fechaTransaccions.OrderBy(x=> x.Fecha);
            
            var ultimaFecha = fechaTransaccions.LastOrDefault();
            var primerFecha = fechaTransaccions.FirstOrDefault();




            var lst = CuentaCL.CuentaConSaldos(companyId, primerFecha.Id, ultimaFecha.Id);
            





            var proximo = (from c in fechaTransaccions orderby c.Fecha descending select new FechaTransaccion(fecha: c.Fecha.AddMonths(1))).FirstOrDefault();

            var ultimo = (from c in fechaTransaccions orderby c.Fecha ascending select new FechaTransaccion(fecha: c.Fecha.AddMonths(-1))).FirstOrDefault();

            return new FechaTransaccion[] { ultimo, proximo };




            ///
        }
    }
}