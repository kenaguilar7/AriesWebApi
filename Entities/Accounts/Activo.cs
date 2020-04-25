using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
    public class Activo : ITipoCuenta
    {
        public TipoCuenta TipoCuenta { get { return TipoCuenta.Activo; } }
        public Comportamiento Comportamiento { get { return Comportamiento.Debito;  } }

        public double SaldoActual(double saldo, double debito, double credito)
        {
            return (saldo + debito - credito);
        }
        public double SaldoMensual(double debito, double credito) {
            return (debito - credito);
        }
    }
}