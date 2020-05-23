using System;
using System.Collections.Generic;
using System.Globalization;
using AriesWebApi.Entities.Entries;

namespace AriesWebApi.Entities.TransactionsDates
{
    public class FechaTransaccion
    {
        public double Id { get; set; }
        public DateTime Fecha { get; set; }
        public bool Cerrada { get; set; }
        public List<Asiento> Asientos {get; set; }
        public FechaTransaccion(){}
        public FechaTransaccion(DateTime fecha, bool cerrada = false, double id = 0)
        {
            Id = id;
            Fecha = fecha;
            Cerrada = cerrada;
        }
        public override string ToString()
        {
            return $"{MonthName(Fecha.Month)} {Fecha.Year}"; 
        }
        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }
    }
}