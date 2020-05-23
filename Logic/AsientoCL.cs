using System.Collections.Generic;
using System.Data;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Entries;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.TransactionsDates;
using AriesWebApi.Entities.Users;

namespace AriesWebApi.Logic {
    public class AsientoCL {
        private readonly AsientosDao asientoDao = new AsientosDao ();

        public IEnumerable<Asiento> GetAll (string companyid,FechaTransaccion fechaTransaccion) {
            return asientoDao.GetPorFecha (companyid,fechaTransaccion);
        }






        public bool Delete (Asiento t, Usuario user, out string mensaje) {
            ///Llamar al api 
            ///Leer el Json 
            ///Deserealizar
            ///retornar a la capa presentación
            return asientoDao.Delete (t, user, out mensaje);

        }
        public Asiento Insert (Asiento t, Usuario user, out string mensaje) {
            return asientoDao.Insert (t, user, out mensaje);
        }
        public bool Update (Asiento t, Usuario user, out string mensaje) {
            return asientoDao.Update (t, user, out mensaje);
        }
        public List<Asiento> GetPorFecha (FechaTransaccion fecha, Compañia compania, bool traerInfoCompleta = false, bool traerNuevo = true) {

            // List<Asiento> lst = asientoDao.GetPorFecha (fecha, compania, traerInfoCompleta);

            // //esto lo unico que hace es generar un nuevo asiento para que se posicione de primer lugar la lista
            // if (traerNuevo) {
            //     var dummy = new Asiento (
            //         numeroAsiento: asientoDao.GetConsecutivo (compania, fecha.Fecha),
            //         compania : compania
            //     ) {
            //         FechaAsiento = fecha
            //     };

            //     lst.Insert (0, dummy);
            // }

            return null;

        }

        public List<Asiento> GetMesesPendientes (FechaTransaccion fecha,
            Compañia compania) {

            var dummy = new List<Asiento> ();

            foreach (var item in GetPorFecha (fecha, compania, traerInfoCompleta : true)) {
                if (item.Estado == EstadoAsiento.Proceso) {
                    dummy.Add (item);
                }
            }

            return dummy;

        }
        public DataTable ReporteAsientos (Compañia compañia, FechaTransaccion fecha, bool traerTodos) {
            return asientoDao.ReporteAsientos (compañia, fecha, traerTodos);
        }
        public DataTable ListadoAsientosDescuadrados (Compañia compañia, FechaTransaccion fechaTransaccion) {
            return asientoDao.ListadoAsientosDescuadrados (compañia, fechaTransaccion);
        }
    }
}