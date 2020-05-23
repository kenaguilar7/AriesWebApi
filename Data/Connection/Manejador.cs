using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AriesWebApi.Data.Connection {
    public class Manejador {
        private MySqlConnection databaseConnection = new MySqlConnection (GetConnectionn ());
        private readonly IConfiguration _configuration;
        public static string GetConnectionn () {

            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.Development.json", optional : true, reloadOnChange : true);
            IConfiguration configuration = builder.Build ();
            return configuration["ConnectionStrings:MySqlConnection"];
        }
        public void OpenConnection () {

            if (databaseConnection.State == ConnectionState.Closed) {
                databaseConnection.Open ();
            }
        }
        public void CloseConnection () {
            if (databaseConnection.State == ConnectionState.Open) {
                databaseConnection.Close ();
            }
        }
        public MySqlConnection GetConnection () {
            OpenConnection ();
            return databaseConnection;
        }
        public int Ejecutar (string nombreSp, List<Parametro> lst, CommandType type) {
            var retorno = 0;

            using (MySqlTransaction tr = GetConnection ().BeginTransaction (IsolationLevel.Serializable)) {

                using (MySqlCommand cmd = new MySqlCommand (nombreSp, databaseConnection, tr)) {

                    cmd.CommandType = type;

                    foreach (var item in lst) {
                        if (item.myDireccion == ParameterDirection.Input) {
                            cmd.Parameters.AddWithValue (item.myNombre, item.myValor);
                        }
                        if (item.myDireccion == ParameterDirection.Output) {
                            cmd.Parameters.Add (item.myNombre, item.myTipoDato, item.myTamanyo).Direction = ParameterDirection.Output;
                        }
                    }

                    try {
                        retorno = cmd.ExecuteNonQuery ();
                        tr.Commit ();
                    } catch (Exception ex) {
                        tr.Rollback ();
                        throw ex;
                    }

                    return retorno;
                }
            }
        }
        public DataTable Listado (String nombreSp, List<Parametro> lst, CommandType type) {
            DataTable dt = new DataTable ();
            MySqlDataAdapter da;
            try {
                da = new MySqlDataAdapter (nombreSp, GetConnection ());
                da.SelectCommand.CommandType = type;
                if (lst != null) {
                    for (int i = 0; i < lst.Count; i++) {
                        da.SelectCommand.Parameters.AddWithValue (lst[i].myNombre, lst[i].myValor);

                    }
                }

                da.Fill (dt);

            } catch (Exception ex) {
                throw ex;
            }
            return dt;
        }
        public DataTable Listado (String nombreSp, CommandType type) {
            return Listado (nombreSp, new List<Parametro> (), type);
        }
        public DataTable Listado (String nombreSp, Parametro parametro, CommandType type) {
            var par = new List<Parametro> ();
            par.Add (parametro);
            return Listado (nombreSp, par, type);
        }
    }
}