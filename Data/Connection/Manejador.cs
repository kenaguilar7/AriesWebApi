using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AriesWebApi.Data.Connection
{
    public class Manejador
    {
        private readonly MySqlConnection _databaseConnection = new MySqlConnection(GetConnectionString());

        public static string GetConnectionString()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            return configuration["ConnectionStrings:MySqlConnection"];
        }

        public void OpenConnection()
        {

            if (_databaseConnection.State == ConnectionState.Closed)
            {
                _databaseConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_databaseConnection.State == ConnectionState.Open)
            {
                _databaseConnection.Close();
            }
        }

        public MySqlConnection GetConnection()
        {
            OpenConnection();
            return _databaseConnection;
        }

        public int Execute(string nombreSp, Parametro parametro)
            => Execute(nombreSp, new List<Parametro> { parametro}, CommandType.Text); 

        public int Execute(string nombreSp, List<Parametro> lst, CommandType type)
        {

            using var connection = new MySqlConnection(GetConnectionString());
            using var cmd = new MySqlCommand(nombreSp, connection);
            connection.Open();

            foreach (var item in lst)
            {

                if (item.myDireccion == ParameterDirection.Input)
                {
                    cmd.Parameters.AddWithValue(item.myNombre, item.myValor);
                }
                if (item.myDireccion == ParameterDirection.Output)
                {
                    cmd.Parameters.Add(item.myNombre, item.myTipoDato, item.myTamanyo).Direction = ParameterDirection.Output;
                }
            }

            var retorno = cmd.ExecuteNonQuery();
            return retorno;
        }

        public long ExecuteAndReturnLastInsertId(string nombreSp, List<Parametro> lst)
        {
            using MySqlConnection connection = new MySqlConnection(GetConnectionString());
            using MySqlCommand cmd = new MySqlCommand(nombreSp, connection);
            connection.Open(); 

            foreach (var item in lst)
            {
                if (item.myDireccion == ParameterDirection.Input)
                {
                    cmd.Parameters.AddWithValue(item.myNombre, item.myValor);
                }
            }

            cmd.ExecuteNonQuery();
            var retorno = cmd.LastInsertedId;
            return retorno;
        }

        public DataTable Listado(String nombreSp, List<Parametro> lst, CommandType type)
        {

            var dtRetorno = new DataTable();
            using (MySqlConnection connect = new MySqlConnection(GetConnectionString()))
            using (var sqlDataAdapter = new MySqlDataAdapter(nombreSp, connect))
            {
                connect.Open();
                foreach (var param in lst)
                {
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(param.myNombre, param.myValor);
                }
                sqlDataAdapter.Fill(dtRetorno);
            }

            return dtRetorno;
        }

        public DataTable Listado(String nombreSp, CommandType type)
        {
            return Listado(nombreSp, new List<Parametro>(), type);
        }

        public DataTable Listado(String nombreSp, Parametro parametro, CommandType type)
        {
            var par = new List<Parametro>
            {
                parametro
            };
            return Listado(nombreSp, par, type);
        }
    }
}