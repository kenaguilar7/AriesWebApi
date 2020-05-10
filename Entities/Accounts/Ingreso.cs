using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
    public class Ingreso : ITipoCuenta
    {
        public NombreTipoCuenta TipoCuenta { get { return NombreTipoCuenta.Ingreso; } }
        public Comportamiento Comportamiento { get { return Comportamiento.Credito; } }
        public decimal SaldoActual(decimal saldo, decimal debito, decimal credito)
        {
            return (saldo - debito + credito);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="debito"></param>
        /// <param name="credito"></param>
        /// <returns></returns>
        public decimal SaldoMensual(decimal debito, decimal credito)
        {
                /// credito - debito
            return (debito + credito);
        }
    }
}
