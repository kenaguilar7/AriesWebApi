using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Interfaces
{
    public interface ITipoCuenta
    {
        TipoCuenta TipoCuenta { get ; }
        Comportamiento Comportamiento { get; }
        decimal SaldoActual(decimal saldo, decimal debito, decimal credito);
        decimal SaldoMensual(decimal debito, decimal credito); 
    }

}
