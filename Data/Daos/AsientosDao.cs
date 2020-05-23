﻿using System;
using System.Collections.Generic;
using System.Data;
using AriesWebApi.Data.Connection;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Entries;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.security;
using AriesWebApi.Entities.TransactionsDates;
using AriesWebApi.Entities.Users;
using MySql.Data.MySqlClient;

namespace AriesWebApi.Data.Daos {
    public class AsientosDao {
        Manejador manejador = new Manejador ();
        public bool Delete (Asiento t, Usuario user, out String mensaje) {
            try {
                if (!Guachi.Consultar (user, VentanaInfo.FormAsientos, CRUDName.Eliminar)) {
                    mensaje = "Acceso denegado!!!";
                    return false;
                }

                var sql = "UPDATE accounting_entries SET active = FALSE " +
                    "WHERE accounting_entry_id = @accounting_entry_id LIMIT 1";

                //int totalActulizados; 
                var cnt = manejador.Ejecutar (sql, new List<Parametro> { new Parametro ("@accounting_entry_id", t.Id) }, CommandType.Text);

                mensaje = $"Se actualizaron {cnt} asientos";
                return true;
            } catch (Exception ex) {
                mensaje = ex.Message;
                return false;
            }
        }
        public Asiento Insert (Asiento t, Usuario user, out String mensaje) {
            if (!Guachi.Consultar (user, VentanaInfo.FormAsientos, CRUDName.Insertar)) {
                mensaje = "Acceso denegado!!!";
                return t;
            }

            var numeroAsiento = GetConsecutivo (t.Compania, t.FechaAsiento.Fecha);

            using (MySqlTransaction tr = manejador.GetConnection ().BeginTransaction (IsolationLevel.Serializable)) {
                try {
                    //si el consecutivo generado no es igual al que tenemos entonces ya alguien mas genero uno
                    //se lanza un excepcion para que el usuario refresque la ventana
                    if (t.NumeroAsiento != numeroAsiento) {
                        throw new Exception ("¡Este asiento ya fue asignado por otro usuario, refresque esta ventana!");
                    }

                    var sqlAsiento = "INSERT INTO accounting_entries(entry_id,accounting_months_id,updated_by) " +
                        "VALUES(@entry_id,@accounting_months_id,@updated_by); SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd = new MySqlCommand (sqlAsiento, tr.Connection, tr)) {

                        cmd.Parameters.AddWithValue ("@entry_id", t.NumeroAsiento);
                        cmd.Parameters.AddWithValue ("@accounting_months_id", t.FechaAsiento.Id);
                        cmd.Parameters.AddWithValue ("@updated_by", user.UsuarioId);
                        var numeroId = cmd.ExecuteScalar ();
                        t.Id = Convert.ToInt32 (numeroId);
                    }

                    tr.Commit ();
                    mensaje = "El asiento se guardó correctamente"; ///TODO
                    return t;
                } catch (Exception) {
                    tr.Rollback ();
                    throw;
                }
            }

        }
        /// <summary>
        /// Lista los asientos por fecha y compañia 
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="compania"></param>
        /// <param name="traerInfoCompleta"></param>
        /// <returns></returns>
        public List<Asiento> GetPorFecha (string companyid,FechaTransaccion fecha) {
            List<Asiento> retorno = new List<Asiento> ();

            var sql = "SELECT AC.accounting_entry_id, AC.entry_id, AC.created_at, AM.month_report, AC.status+0, AC.convalidated_at, AM.closed FROM accounting_months AS AM " +
                "JOIN accounting_entries AS AC ON AM.accounting_months_id = AC.accounting_months_id AND AM.closed = false AND AC.convalidated = false AND AM.active = true AND AC.active = true " +
                "AND MONTH(AM.month_report) = @month_report AND YEAR(AM.month_report) = @year_report AND AM.company_id = @company_id GROUP BY AC.entry_id ORDER BY AC.entry_id DESC";

            DataTable dt = manejador.Listado (sql,
                new List<Parametro> {
                    new Parametro ("@month_report", fecha.Fecha.Month),
                    new Parametro ("@year_report", fecha.Fecha.Year),
                    new Parametro ("@company_id", companyid)
                },
                CommandType.Text
            );

            foreach (DataRow item in dt.Rows) {

                Object[] vs = item.ItemArray;
                var asiento = new Asiento ();
                asiento.Id = Convert.ToInt32 (vs[0]);
                asiento.NumeroAsiento = Convert.ToInt32 (vs[1]);
                asiento.FechaRegistro = Convert.ToDateTime (vs[2]);
                asiento.FechaAsiento = new FechaTransaccion (fecha: Convert.ToDateTime (vs[3])); //agregar el resto de parametros
                //asiento.Convalidado = Convert.ToBoolean();
                asiento.Estado = (EstadoAsiento) Convert.ToInt32 (vs[4]);
                // asiento.Compania = compania;
                asiento.FechaAsiento = fecha;
                // if (traerInfoCompleta) {
                //     asiento.Transaccions = new TransaccionDao ().GetCompleto (asiento);
                // }
                retorno.Add (asiento);
            }
            return retorno;
        }

        public DataTable ListadoAsientosDescuadrados (Compañia compañia, FechaTransaccion fechaTransaccion) {
            var parametros = new List<Parametro> {
                new Parametro ("@company_id", compañia.Codigo),
                new Parametro ("@fecha_contable", $"{fechaTransaccion.Fecha.Year}{String.Format("{0, 0:D2}", fechaTransaccion.Fecha.Month)}")
            };
            var sql = "SELECT fecha_contable, entry_id, debito, credito, status FROM estado_asientos WHERE company_id = @company_id AND fecha_contable = @fecha_contable AND debito <> credito; ";

            return manejador.Listado (sql, parametros, CommandType.Text);

        }

        /// <summary>
        /// con esta función vamos a generar un nuevo consecutivo para los asientos, 
        /// la idea es que se pueda comparar con el  que tenemos para que los datos 
        /// sean lo mas exactos posibles. 
        /// </summary>
        /// <param name="compania"></param>
        /// <param name="mesCurso"></param>
        /// <param name="tr"></param>
        /// <returns></returns>
        public int GetConsecutivo (Compañia compania, DateTime mesCurso) {
            var sql = "SELECT AC.entry_id+1 FROM accounting_months AS AM " +
                "JOIN accounting_entries AS AC ON AM.accounting_months_id = AC.accounting_months_id AND AC.convalidated = false " +
                "AND MONTH(AM.month_report) = @month_report AND YEAR(AM.month_report) = @year_report AND AM.company_id = @company_id GROUP BY AC.entry_id ORDER BY AC.entry_id DESC LIMIT 1";
            using (MySqlTransaction tr = manejador.GetConnection ().BeginTransaction (IsolationLevel.Serializable)) {
                using (MySqlCommand cmd = new MySqlCommand (sql, tr.Connection, tr)) {
                    cmd.Parameters.AddWithValue ("@company_id", compania.Codigo);
                    cmd.Parameters.AddWithValue ("@month_report", mesCurso.Month);
                    cmd.Parameters.AddWithValue ("@year_report", mesCurso.Year);
                    MySqlDataAdapter da = new MySqlDataAdapter {
                        SelectCommand = cmd
                    };
                    //el siguiente codigo ayuda cuando es el primer registro
                    //al ser el primero la bbdd ratorna vacio y el programa interpreta como nulo
                    //si es nulo cre uno nuevo
                    var retorno = "";
                    if (cmd.ExecuteScalar () != null) {
                        retorno = cmd.ExecuteScalar ().ToString ();
                    } else {
                        retorno = "1";
                    }
                    return Convert.ToInt32 (retorno);
                }
            }
        }
        /// <summary>
        /// Se actualiza 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update (Asiento t, Usuario user, out String mensaje) {
            if (!Guachi.Consultar (user, VentanaInfo.FormAsientos, CRUDName.Actualizar)) {
                mensaje = "Acceso denegado!!!";
                return false;
            }

            var sqlAsiento = "UPDATE accounting_entries SET updated_by = @updated_by, status = @status WHERE accounting_entry_id = @accounting_entry_id LIMIT 1; ";
            List<Parametro> par = new List<Parametro> ();
            par.Add (new Parametro ("@updated_by", user.UsuarioId));
            par.Add (new Parametro ("@status", Convert.ToInt32 (t.Estado)));
            par.Add (new Parametro ("@accounting_entry_id", t.Id));

            try {

                if (manejador.Ejecutar (sqlAsiento, par, CommandType.Text) > 0) {
                    mensaje = "asiento actualizado correctamente";
                    return true;
                } else {
                    mensaje = $"El asiento {t.NumeroAsiento} no pudo ser actualizado";
                    return false;
                }

            } catch (Exception ex) {
                mensaje = ex.Message;
                return false;
            }

        }
        public DataTable ReporteAsientos (Compañia compañia, FechaTransaccion fecha, Boolean traerTodos) {
            var sql = "";
            var parametros = new List<Parametro> ();

            if (traerTodos) {
                sql = "SET lc_time_names = 'es_MX'; " +
                    "SELECT DATE_FORMAT(month_report, '%M %y') AS month_report,entry_id,account_name,reference,detail,bill_date,debit,credit,money_type, money_chance,balance_usd FROM accounting_entries_info WHERE " +
                    "company_id = @company_id ";
                parametros.Add (new Parametro ("@company_id", compañia.Codigo));

            } else {
                sql = "SET lc_time_names = 'es_MX'; " +
                    "SELECT DATE_FORMAT(month_report, '%M %y') AS month_report,entry_id,account_name,reference,detail,bill_date,debit,credit,money_type, money_chance,balance_usd FROM accounting_entries_info WHERE " +
                    "company_id = @company_id AND DATE_FORMAT(month_report, '%Y%m') = @month_report ";
                parametros.Add (new Parametro ("@company_id", compañia.Codigo));
                parametros.Add (new Parametro ("@month_report", $"{fecha.Fecha.Year}{String.Format("{0, 0:D2}", fecha.Fecha.Month)}"));

            }
            return manejador.Listado (sql, parametros, CommandType.Text);
        }
    }
}