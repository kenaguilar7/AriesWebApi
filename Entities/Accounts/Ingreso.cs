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
            return (saldo + credito - debito);
        }
        
        public decimal SaldoMensual(decimal debito, decimal credito)
        {
                /// credito - debito
            return (credito - debito);
        }
    }
}
