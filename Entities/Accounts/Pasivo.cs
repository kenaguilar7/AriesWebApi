using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
    public class Pasivo : ITipoCuenta
    {
        public TipoCuenta TipoCuenta { get { return TipoCuenta.Pasivo; } }
        public Comportamiento Comportamiento { get { return Comportamiento.Credito; } }
        public double SaldoActual(double saldo, double debito, double credito)
        {
            return (saldo - debito + credito);
        }
        public double SaldoMensual(double debito, double credito)
        {
            ///credito - debito
            return (debito + credito);
        }
    }
}
