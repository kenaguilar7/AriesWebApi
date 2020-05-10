using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
    public class Egreso : ITipoCuenta
    {
        public NombreTipoCuenta TipoCuenta { get { return NombreTipoCuenta.Egreso; } }
        public Comportamiento Comportamiento { get { return Comportamiento.Debito; } }
        public decimal SaldoActual(decimal saldo, decimal debito, decimal credito)
        {
            return (saldo + debito - credito);
        }
        public decimal SaldoMensual(decimal debito, decimal credito)
        {
            return (credito - debito);
        }
    }
}
