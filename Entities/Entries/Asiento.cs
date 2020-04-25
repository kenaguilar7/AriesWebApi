using System;
using System.Collections.Generic;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.TransactionsDates;

namespace AriesWebApi.Entities.Entries
{
    public class Asiento
    {
       public Double Id { get; set; }
        public int NumeroAsiento { get; set; }//cambiar a int 
        public List<Transaccion> Transaccions { get; set; }
        public DateTime FechaRegistro { get; set; }//cambiar a mes de registri // YY/MM/DDD
        public FechaTransaccion FechaAsiento { get; set; }//Quitar
        public Boolean Convalidado { get; set; }
        public DateTime FechaConvalidacion { get; set; }
        public Compañia Compania { get; set; }//Quitar
        public EstadoAsiento Estado { get; set; }


        public Asiento(int numeroAsiento, List<Transaccion> transaccions, DateTime fechaRegistro, FechaTransaccion fechaAiento,
                       Compañia compania, EstadoAsiento estado = EstadoAsiento.Proceso, Double Id = 0)
        {
            
            this.NumeroAsiento = numeroAsiento;
            this.Transaccions = transaccions;
            this.FechaRegistro = fechaRegistro;
            this.FechaAsiento = fechaAiento;
            this.Convalidado = Convalidado;
            this.Compania = compania;
            this.Id = Id;
        }

        public Asiento(int numeroAsiento, Compañia compania)
        {
            NumeroAsiento = numeroAsiento;
            Compania = compania;
            this.Transaccions = new List<Transaccion>();
        }

        public Asiento()
        {
        }

        public override string ToString()
        {
            return Convert.ToString(NumeroAsiento);
        }
        public double Debitos
        {
            get
            {
                var retorno = 0.00;

                foreach (var item in Transaccions)
                {
                    if (item.ComportamientoCuenta == Comportamiento.Debito)
                    {
                        retorno += item.Monto;
                    }
                }

                return retorno;
            }
        }

        public double Creditos
        {
            get
            {

                var retorno = 0.00;

                foreach (var item in Transaccions)
                {
                    if (item.ComportamientoCuenta == Comportamiento.Credito)
                    {
                        retorno += item.Monto;
                    }
                }

                return retorno;
            }
        }

        public Boolean Cuadrado
        {
            get
            {

                if (Debitos == Creditos)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }  
    }
}