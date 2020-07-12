using AriesWebApi.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AriesWebApi.Entities.TransactionsDates.TransactionDateList
{
    public static class ManageTransactionDateList
    {

        public static FechaTransaccion GetOlderDate(this IEnumerable<FechaTransaccion> fechaTransaccions)
            => fechaTransaccions.OrderBy(c => c.Fecha).FirstOrDefault();

        public static FechaTransaccion GetNewerDate(this IEnumerable<FechaTransaccion> fechaTransaccions)
            => fechaTransaccions.OrderByDescending(c => c.Fecha).FirstOrDefault();

    }
}
