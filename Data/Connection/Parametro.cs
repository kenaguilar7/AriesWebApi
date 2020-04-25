using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace AriesWebApi.Data.Connection
{
    public class Parametro
    {
        public Parametro(string nombre, object valor)
        {
            this.nombre = nombre;
            this.valor = valor;
            this.direction = ParameterDirection.Input;
        }
        public Parametro(string nombre, MySqlDbType tipoDato, int tamanyo)
        {
            this.nombre = nombre;
            this.tipoDato = tipoDato;
            this.tamanyo = tamanyo;
            this.direction = ParameterDirection.Output;
        }

        public string myNombre
        {
            set { nombre = value; }
            get { return nombre; }
        }
        public Object myValor
        {
            set { valor = value; }
            get { return valor; }
        }
        public MySqlDbType myTipoDato
        {
            set { tipoDato = value; }
            get { return tipoDato; }
        }
        public int myTamanyo
        {
            set { tamanyo = value; }
            get { return tamanyo; }
        }
        public ParameterDirection myDireccion
        {
            set { direction = value; }
            get { return direction; }
        }

        private string nombre;
        private Object valor;
        private MySqlDbType tipoDato;
        private int tamanyo;
        private ParameterDirection direction;
    }
}