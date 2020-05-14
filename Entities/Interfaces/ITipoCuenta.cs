using AriesWebApi.Entities.Accounts;
using AriesWebApi.Entities.Enums;
using JsonSubTypes;
using Newtonsoft.Json;

namespace AriesWebApi.Entities.Interfaces {
    [JsonConverter (typeof (JsonSubtypes), "TipoCuenta")]
    [JsonSubtypes.KnownSubType (typeof (Activo), "1")]
    [JsonSubtypes.KnownSubType (typeof (Pasivo), "2")]
    [JsonSubtypes.KnownSubType (typeof (Patrimonio), "3")]
    [JsonSubtypes.KnownSubType (typeof (Ingreso), "4")]
    [JsonSubtypes.KnownSubType (typeof (CostoVenta), "5")]
    [JsonSubtypes.KnownSubType (typeof (Egreso), "6")]
    public interface ITipoCuenta {
        NombreTipoCuenta TipoCuenta { get; }
        Comportamiento Comportamiento { get; }
        decimal SaldoActual (decimal saldo, decimal debito, decimal credito);
        decimal SaldoMensual (decimal debito, decimal credito);
    }
}