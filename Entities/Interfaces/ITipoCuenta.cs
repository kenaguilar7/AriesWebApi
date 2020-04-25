using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Interfaces
{
    public interface ITipoCuenta
    {
        TipoCuenta TipoCuenta { get ; }
        Comportamiento Comportamiento { get; }
        double SaldoActual(double saldo, double debito, double credito);
        double SaldoMensual(double debito, double credito); 
    }

}
