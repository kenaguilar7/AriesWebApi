using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;
using System.Collections.Generic;

namespace AriesWebApi.Entities.Accounts
{
    public interface ICuenta
    {
        bool Active { get; set; }
        decimal CreditosColones { get; set; }
        decimal CreditosDolares { get; set; }
        bool Cuadrada { get; set; }
        decimal DebitosColones { get; set; }
        decimal DebitosDolares { get; set; }
        string Detalle { get; set; }
        bool Editable { get; set; }
        int Id { get; set; }
        IndicadorCuenta Indicador { get; set; }
        Compañia MyCompania { get; set; }
        string Nombre { get; set; }
        int Padre { get; set; }
        string PathDirection { get; set; }
        decimal SaldoActualColones { get; }
        decimal SaldoActualDolares { get; }
        decimal SaldoAnteriorColones { get; set; }
        decimal SaldoAnteriorDolares { get; set; }
        decimal SaldoMensualColones { get; }
        decimal SaldoMensualDolares { get; }
        ITipoCuenta TipoCuenta { get; set; }

        bool CuentaConMovientos();
        Cuenta DeepCopy();
        string[] GetArrayNameAndKey(List<Cuenta> list);
        string[] GetNombreParaExcel(List<Cuenta> list);
        decimal SaldoAnteriorParaExcel(short tipo);
        string ToString();
        bool Validate(IValidator<Cuenta> validator, ref IEnumerable<string> brokenRules);
    }
}