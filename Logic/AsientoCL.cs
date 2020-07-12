using System;
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

        public IEnumerable<Asiento> GetAll (double  fechaTransaccionId) {
            return asientoDao.GetAll (fechaTransaccionId);
        }

        public Asiento GetPreEntry(string companyId,FechaTransaccion dateParams){
            
            var asiento = new Asiento();
            asiento.NumeroAsiento =  asientoDao.GetConsecutivo(companyId,dateParams.Fecha);
            asiento.FechaAsiento = dateParams;
            return asiento; 
        }

        public Asiento Insert (string companyId, double fechaTransaccionId,int userId, Asiento asiento) {
            return asientoDao.Insert (companyId, fechaTransaccionId, userId,asiento);
        }

        public void Update (string companyId, double fechaTransaccionId,int userId, Asiento asiento) {
             asientoDao.Update (companyId, fechaTransaccionId, userId,asiento);
        }

        public void Delete (string companyid, double accountingperiodid,int userId, double asientoid) {
             asientoDao.Delete (asientoid);
        }
        
    }
}