using System;
using System.Collections.Generic;
using System.Data;
using AriesWebApi.Data.Connection;
using AriesWebApi.Entities.Accounts;
using AriesWebApi.Entities.Entries;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.security;
using AriesWebApi.Entities.Users;
using MySql.Data.MySqlClient;

namespace AriesWebApi.Data.Daos {

    public class TransaccionDao {

        Manejador manejador = new Manejador ();

        public Transaccion Insert (Transaccion transaccion, decimal idAsiento, int userId) {

            using (MySqlTransaction tr = manejador.GetConnection ().BeginTransaction (IsolationLevel.Serializable)) {
                try {
                    var sql = "INSERT INTO transactions_accounting " +
                        "(account_id,accounting_entry_id,reference,detail,balance, balance_type,money_type, money_chance,bill_date, updated_by) " +
                        "VALUES (@account_id,@accounting_entry_id,@reference,@detail,@balance,@balance_type,@money_type,@money_chance,@bill_date,@updated_by); SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd = new MySqlCommand (sql, tr.Connection, tr)) {
                        List<Parametro> lst = new List<Parametro> ();
                        cmd.Parameters.AddWithValue ("@account_id", transaccion.CuentaDeAsiento.Id);
                        cmd.Parameters.AddWithValue ("@accounting_entry_id", idAsiento);
                        cmd.Parameters.AddWithValue ("@reference", transaccion.Referencia);
                        cmd.Parameters.AddWithValue ("@detail", transaccion.Detalle);
                        cmd.Parameters.AddWithValue ("@balance", transaccion.Monto);
                        cmd.Parameters.AddWithValue ("@balance_type", Convert.ToInt16 (transaccion.ComportamientoCuenta));
                        cmd.Parameters.AddWithValue ("@money_type", Convert.ToInt16 (transaccion.TipoCambio));
                        cmd.Parameters.AddWithValue ("@money_chance", transaccion.MontoTipoCambio);
                        cmd.Parameters.AddWithValue ("@bill_date", transaccion.FechaFactura);
                        cmd.Parameters.AddWithValue ("@updated_by", userId);

                        var id = cmd.ExecuteScalar ();
                        transaccion.Id = Convert.ToInt32 (id);
                    }
                    tr.Commit ();
                    return transaccion;
                } catch (Exception) {
                    tr.Rollback ();
                    throw;
                }
            }
        }
        public void Update (Transaccion tr, long userid) {

            var sql = "UPDATE transactions_accounting " +
                "SET account_id = @account_id, " +
                "reference = @reference, " +
                "detail = @detail, " +
                "balance = @balance," +
                "balance_type = @balance_type, " +
                "money_type = @money_type, " +
                "money_chance = @money_chance, " +
                "bill_date = @bill_date," +
                "updated_by = @updated_by " +
                "WHERE transaction_accounting_id = @transaction_accounting_id";

            List<Parametro> lst = new List<Parametro> {
                new Parametro ("@account_id", tr.CuentaDeAsiento.Id),
                new Parametro ("@reference", tr.Referencia),
                new Parametro ("@detail", tr.Detalle),
                new Parametro ("@balance", tr.Monto),
                new Parametro ("@balance_type", (int) tr.ComportamientoCuenta),
                new Parametro ("@money_type", (int) tr.TipoCambio),
                new Parametro ("@money_chance", tr.MontoTipoCambio),
                new Parametro ("@bill_date", tr.FechaFactura),
                new Parametro ("@updated_by", userid),
                new Parametro ("@transaction_accounting_id", tr.Id)
            };

            try {
                var affectedRows = manejador.Ejecutar (sql, lst, CommandType.Text);
                if (affectedRows == 0) { throw new Exception ("No se actualizo ningun registro"); }
            } catch (Exception ex) {
                throw ex;
            }

        }
        public List<Transaccion> GetCompleto (int bookEntryId) {

            var retorno = new List<Transaccion> ();

            var sql = "select " +
                "TA.transaction_accounting_id, " + //0
                "TA.reference, " + //1
                "TA.detail, " + //2
                "TA.bill_date, " + //3
                "TA.balance, " + //4
                "TA.balance_type+0, " + //5
                "TA.money_type+0, " + //6
                "TA.money_chance, " + //7
                "AN.name, " + //8
                "AC.account_id, " + //9
                "AC.father_account, " + //10
                "AC.account_guide+0, " +
                " GETFULLPATH(AC.account_id) " +
                "FROM transactions_accounting AS TA JOIN accounting_entries AS AE ON TA.accounting_entry_id = AE.accounting_entry_id AND AE.accounting_entry_id = @accounting_entry_id " +
                "JOIN accounts AS AC ON TA.account_id = AC.account_id JOIN accounts_names AS AN ON AC.account_name_id = AN.account_name_id " +
                "WHERE TA.active = 1 AND AE.active = 1 AND AC.active = 1 ; ";

            DataTable dt = manejador.Listado (sql, new Parametro ("@accounting_entry_id", bookEntryId), CommandType.Text);

            foreach (DataRow item in dt.Rows) {
                Object[] vs = item.ItemArray;
                Transaccion tran = new Transaccion (
                    id: Convert.ToInt32 (vs[0]),
                    referencia: Convert.ToString (vs[1]),
                    detalle: Convert.ToString (vs[2]),
                    fechaFactura: Convert.ToDateTime (vs[3]),
                    balance: Convert.ToDecimal (vs[4]),
                    comportamientoCuenta: ((Comportamiento) Convert.ToInt32 (vs[5])),
                    tipoCambio: ((TipoCambio) Convert.ToInt16 (vs[6])),
                    montoTipoCambio : Convert.ToDecimal (vs[7]),
                    cuentaDeAsiento : new Cuenta {
                        Nombre = Convert.ToString (vs[8]),
                            Id = Convert.ToInt32 (vs[9]),
                            Padre = Convert.ToInt32 (vs[10]),
                            Indicador = (IndicadorCuenta) Convert.ToInt32 (vs[11]),
                            PathDirection = Convert.ToString (vs[12]),
                    }
                );

                retorno.Add (tran);

            }

            return retorno;

        }
        public void Delete (long transaccionId, long idAsiento, long userId) {

            var sql = "UPDATE transactions_accounting SET active = 0, updated_by = @updated_by " +
                "WHERE transaction_accounting_id = @transaction_accounting_id " +
                "AND accounting_entry_id = @accounting_entry_id";

            using (MySqlCommand cmd = new MySqlCommand (sql, manejador.GetConnection ())) {

                cmd.Parameters.AddWithValue ("@updated_by", userId);
                cmd.Parameters.AddWithValue ("@transaction_accounting_id", transaccionId);
                cmd.Parameters.AddWithValue ("@accounting_entry_id", idAsiento);

                try {

                    var cont = cmd.ExecuteNonQuery ();
                    if (cont == 0) {
                        throw new Exception ("No se pudo eliminar este registro");
                    }

                } catch (Exception) {
                    throw;
                }
            }

        }

    }
}