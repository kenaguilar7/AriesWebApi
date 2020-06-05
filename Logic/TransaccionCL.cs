using System.Collections.Generic;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.Entries;
using AriesWebApi.Entities.Users;

namespace AriesWebApi.Logic {
    public class TransaccionCL {
        TransaccionDao transaccionDao = new TransaccionDao ();
        public Transaccion Insert (Transaccion transaccion, decimal asientoId, int userId) => transaccionDao.Insert (transaccion, asientoId, userId);
        public IEnumerable<Transaccion> GetCompleto (int bookEntryId) => transaccionDao.GetCompleto (bookEntryId);
        public void Update (Transaccion transaction, long userid) => transaccionDao.Update (transaction, userid);
        public void Delete (long transaccionId, long asientoId, long userId) => transaccionDao.Delete (transaccionId, asientoId, userId);

    }
}