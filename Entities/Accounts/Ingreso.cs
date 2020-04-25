using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
    public class Ingreso : ITipoCuenta
    {
        public TipoCuenta TipoCuenta { get { return TipoCuenta.Ingreso; } }
        public Comportamiento Comportamiento { get { return Comportamiento.Credito; } }
        public double SaldoActual(double saldo, double debito, double credito)
        {
            return (saldo - debito + credito);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="debito"></param>
        /// <param name="credito"></param>
        /// <returns></returns>
        public double SaldoMensual(double debito, double credito)
        {
                /// credito - debito
            return (debito + credito);
        }
    }
}
