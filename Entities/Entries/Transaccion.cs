using System;
using AriesWebApi.Entities.Accounts;
using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Entries
{
    public class Transaccion
    {
        public Double Id { get; set; }
        public Cuenta CuentaDeAsiento { get; set; }
        public String Referencia { get; set; }
        public String Detalle { get; set; }
        public DateTime FechaFactura { get; set; }
        public Double Monto { get; set; }
        public Comportamiento ComportamientoCuenta { get; set; }
        public Double MontoTipoCambio { get; set; }
        public Transaccion(Cuenta cuentaDeAsiento, string referencia, string detalle, DateTime fechaFactura,
                           double balance, Comportamiento comportamientoCuenta = Comportamiento.Credito, Double id = 0, Double montoTipoCambio = 1.00)
        {
            CuentaDeAsiento = cuentaDeAsiento;
            Referencia = referencia;
            Detalle = detalle;
            FechaFactura = fechaFactura;
            Monto = balance;
            ComportamientoCuenta = comportamientoCuenta;
            this.Id = id;
            MontoTipoCambio = montoTipoCambio;
        }
        public Transaccion() {
            MontoTipoCambio = 1.00;
        }

        public TipoCambio TipoCambio {

            get
            {
                if (MontoTipoCambio == 1)
                {
                    return TipoCambio.Colones;
                }
                else {
                    return TipoCambio.Dolares;
                }
            }
        }
    }
}
