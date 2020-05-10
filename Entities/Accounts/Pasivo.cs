using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
    public class Pasivo : ITipoCuenta
    {
        public NombreTipoCuenta TipoCuenta { get { return NombreTipoCuenta.Pasivo; } }
        public Comportamiento Comportamiento { get { return Comportamiento.Credito; } }
        public decimal SaldoActual(decimal saldo, decimal debito, decimal credito)
        {
            return (saldo - debito + credito);
        }
        public decimal SaldoMensual(decimal debito, decimal credito)
        {
            ///credito - debito
            return (debito + credito);
        }
    }
}
