using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
        
    public class Activo : ITipoCuenta
    {
        public NombreTipoCuenta TipoCuenta { get { return NombreTipoCuenta.Activo; } }
        public Comportamiento Comportamiento { get { return Comportamiento.Debito;  } }

        public decimal SaldoActual(decimal saldo, decimal debito, decimal credito)
        {
            return (saldo + debito - credito);
        }
        public decimal SaldoMensual(decimal debito, decimal credito) {
            return (debito - credito);
        }
    }
}