using System.Collections.Generic;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;

namespace AriesWebApi.Entities.Accounts
{
    public class Cuenta
    {
        public string Nombre { get; set; }
        public Compañia MyCompania { get; set; } ///Quitar
        public double Id { get; set; }
        public double Padre { get; set; }
        public double SaldoAnteriorColones { get; set; }
        public double SaldoAnteriorDolares { get; set; }
        public double DebitosColones { get; set; }
        public double DebitosDolares { get; set; }
        public double CreditosColones { get; set; }
        public double CreditosDolares { get; set; }
        public string Detalle { get; set; }
        public bool Active { get; set; }
        public IndicadorCuenta Indicador { get; set; }
        public ITipoCuenta TipoCuenta { get; set; }
        public bool Editable { get; set; }
        public string PathDirection { get; set; }
        public bool Cuadrada { get; set; }
        public Cuenta()
        {

        }
        public Cuenta(string nombre, Compañia myCompania, ITipoCuenta tipoCuenta, IndicadorCuenta indicador, string detalle,
                      double padre, double id = 0, double saldoAnteriorColones = 0.0, double saldoAnteriorDolares = 0.00,
                      double debitos = 0.0, double creditos = 0.0, bool active = true, bool editable = false, bool cuadrada = true)
        {
            Nombre = nombre;
            MyCompania = myCompania;
            Id = id;
            Padre = padre;
            SaldoAnteriorColones = saldoAnteriorColones;
            SaldoAnteriorDolares = SaldoAnteriorDolares;
            DebitosColones = debitos;
            CreditosColones = creditos;
            Detalle = detalle;
            Active = active;
            TipoCuenta = tipoCuenta;
            Indicador = indicador;
            Editable = editable;
            Cuadrada = cuadrada; 
        }
        public Cuenta(string nombre, double id, double padre, IndicadorCuenta indicadorCuenta = IndicadorCuenta.Cuenta_Auxiliar)
        {
            Nombre = nombre;
            Id = id;
            Padre = padre;
            Indicador = indicadorCuenta;
        }
        public double SaldoActualColones
        {
            get { return TipoCuenta.SaldoActual(this.SaldoAnteriorColones, this.DebitosColones, this.CreditosColones); }
        }
        public double SaldoMensualColones
        {
            get { return TipoCuenta.SaldoMensual(this.DebitosColones, this.CreditosColones); }
        }
        public double SaldoActualDolares
        {
            get { return TipoCuenta.SaldoActual(this.SaldoAnteriorDolares, DebitosDolares, CreditosDolares); }
        }
        public double SaldoMensualDolares
        {
            get { return TipoCuenta.SaldoMensual(this.DebitosDolares, this.CreditosDolares); }
        }
        public override string ToString()
        {
            return Nombre;
        }
        public static ITipoCuenta GenerarTipoCuenta(int type)
        {
            switch (type)
            {
                case 1: return new Activo();
                case 2: return new Pasivo();
                case 3: return new Patrimonio();
                case 4: return new Ingreso();
                case 5: return new CostoVenta();
                case 6: return new Egreso();
                default: return null;

            }
        }
        /// <summary>
        /// Usar solo para los reportes de excel 
        /// mandar por parametro un 1 si se desea mostar el campo de debitos 
        /// y un dos si desea momstar el campo de debitos
        /// el sistema se encaraga de procesar la informarcion
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public double SaldoAnteriorParaExcel(int tipo)
        {

            if (this.TipoCuenta.Comportamiento == (Comportamiento)tipo)
            {
                return SaldoAnteriorColones;
            }
            else
            {
                return 0.00;
            }


        }
        public string[] GetNombreParaExcel(List<Cuenta> list)
        {

            ///BUscar cuantos padres tiene 
            var dummy = this;
            var listaRetorno = new List<Cuenta>();

            while ((dummy = BuscarCuentaPadre(dummy)) != null)
            {

                listaRetorno.Add(dummy);
            }

            if (listaRetorno.Count == 0)
            {
                var retorno = new string[1];
                retorno[0] = this.Nombre;
                for (int i = 0; i < retorno.Length; i++)
                {
                    if (retorno[i] == null)
                    {
                        retorno[i] = "";
                    }
                }
                return retorno;
            }
            else
            {
                var retorno = new string[listaRetorno.Count + 1];

                retorno[retorno.Length - 1] = this.Nombre;
                for (int i = 0; i < retorno.Length; i++)
                {
                    if (retorno[i] == null)
                    {
                        retorno[i] = "";
                    }
                }
                return retorno;
            }

            Cuenta BuscarCuentaPadre(Cuenta cuentaHija)
            {
                if (list.Count != 0)
                {
                    foreach (Cuenta item in list)
                    {
                        if (item.Id == cuentaHija.Padre)
                        {
                            return item;
                        }

                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }
        public string[] GetArrayNameAndKey(List<Cuenta> list)
        {

            ///BUscar cuantos padres tiene 
            var dummy = this;
            var listaRetorno = new List<Cuenta>();

            while ((dummy = BuscarCuentaPadre(dummy)) != null)
            {

                listaRetorno.Add(dummy);
            }

            if (listaRetorno.Count == 0)
            {
                var retorno = new string[1];
                retorno[0] = this.Nombre;
                for (int i = 0; i < retorno.Length; i++)
                {
                    if (retorno[i] == null)
                    {
                        retorno[i] = "";
                    }
                }
                return retorno;
            }
            else
            {
                var retorno = new string[listaRetorno.Count + 1];

                retorno[retorno.Length - 1] = this.Nombre;
                for (int i = 0; i < retorno.Length; i++)
                {
                    if (retorno[i] == null)
                    {
                        retorno[i] = "";
                    }
                }
                return retorno;
            }

            Cuenta BuscarCuentaPadre(Cuenta cuentaHija)
            {
                if (list.Count != 0)
                {
                    foreach (Cuenta item in list)
                    {
                        if (item.Id == cuentaHija.Padre)
                        {
                            return item;
                        }

                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }
        public Cuenta DeepCopy()
        {
            Cuenta other = (Cuenta)this.MemberwiseClone();

            other.Nombre = string.Copy(Nombre);
            other.MyCompania = MyCompania;
            other.Id = Id;
            other.Padre = Padre;
            other.SaldoAnteriorColones = SaldoAnteriorColones;
            other.SaldoAnteriorDolares = SaldoAnteriorDolares;
            other.DebitosColones = DebitosColones;
            other.DebitosDolares = DebitosDolares;
            other.CreditosColones = CreditosColones;
            other.CreditosDolares = CreditosDolares;
            other.Active = Active;
            other.Indicador = Indicador;
            other.TipoCuenta = TipoCuenta;

            return other;
        }
        /// <summary>
        /// Si la cuenta tiene algun tipo de movientos
        /// ya sea en Debitos, Creditos o saldo actual devolvera true
        /// </summary>
        /// <returns></returns>
        public bool CuentaConMovientos()
        {
            if (SaldoAnteriorColones == 0.00 && DebitosColones == 0.00 && CreditosColones == 0.00)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }

}