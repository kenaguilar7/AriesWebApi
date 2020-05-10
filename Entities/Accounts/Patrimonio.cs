using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
    public class Patrimonio :  ITipoCuenta
    {
        public NombreTipoCuenta TipoCuenta { get { return NombreTipoCuenta.Patrimonio; } }
        public Comportamiento Comportamiento { get { return Comportamiento.Credito; } }
        public decimal SaldoActual(decimal saldo, decimal debito, decimal credito)
        {
            return (saldo - debito + credito);
        }
        public decimal SaldoMensual(decimal debito, decimal credito)
        {
            //Cresdito - debito
            return (debito + credito);
        }
    }
}
