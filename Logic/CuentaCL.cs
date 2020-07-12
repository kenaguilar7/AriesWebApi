using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.Accounts;
using AriesWebApi.Entities.Accounts.AccountList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AriesWebApi.Logic
{
    public class CuentaCL
    {
        private readonly CuentaDao _cuentadao = new CuentaDao();

        public List<Cuenta> GetAll(string companyid)
        => _cuentadao.GetAll(companyid);

        public Cuenta Insert(string companyId, Cuenta cuenta, int userId)
        {
            var cuentaPadre = GetAll(companyId).FirstOrDefault(x => x.Id == cuenta.Padre);
            return _cuentadao.Insert(companyId, cuenta, cuentaPadre, userId);
        }

        public void Update(string companyId, Cuenta cuenta, int userId)
        {
            _cuentadao.UpdateNameInfo(companyId, cuenta, userId);
        }

        public void Delete(string companyId, double accountId, int userId)
        {
            _cuentadao.Delete(companyId, accountId, userId);
        }

        public Cuenta CuentaConSaldos(string companyId, double accountId,
                                      double fromAccountPeriodId, double toAccountPeriodId)
        {
            IEnumerable<Cuenta> accounts = GetAll(companyId).GetLowLevelAccounts(accountId);
            BuildBalanceAccount(companyId, fromAccountPeriodId, toAccountPeriodId, accounts);
            return accounts.First(row => row.Id == accountId);
        }

        public IEnumerable<Cuenta> CuentaConSaldos(string companyId, double fromAccountPeriodId,
                                                   double toAccountPeriodId)
        {
            var accounts = GetAll(companyId);
            return BuildBalanceAccount(companyId, fromAccountPeriodId, toAccountPeriodId, accounts);
        }

        private IEnumerable<Cuenta> BuildBalanceAccount(string companyid, double fromAccountPeriodId,
                                                        double toAccountPeriodId, IEnumerable<Cuenta> lstCuentas)
        {
            var TableWithAllAccountsBalance = _cuentadao.CuentasConSaldos(companyid, fromAccountPeriodId, toAccountPeriodId);
            FillAuxiliarsAccountsWithBalance(lstCuentas, TableWithAllAccountsBalance);
            return lstCuentas.BuildAccountsBalance();
        }

        private void FillAuxiliarsAccountsWithBalance(IEnumerable<Cuenta> accountsToFill, DataTable TableWithAllAccountsBalance)
        {
            foreach (Cuenta cuenta in accountsToFill)
            {
                cuenta.DebitosColones = (Decimal)TableWithAllAccountsBalance.AsEnumerable()
                    .Where(row => row.Field<UInt32>("account_id") == cuenta.Id)
                    .Sum(row => row.Field<Double>("SUM(debito)"));

                cuenta.DebitosDolares = (Decimal)TableWithAllAccountsBalance.AsEnumerable()
                    .Where(row => row.Field<UInt32>("account_id") == cuenta.Id)
                    .Sum(row => row.Field<Double>("SUM(debito_USD)"));

                cuenta.CreditosColones = (Decimal)TableWithAllAccountsBalance.AsEnumerable()
                    .Where(row => row.Field<UInt32>("account_id") == cuenta.Id)
                    .Sum(row => row.Field<Double>("SUM(credito)"));

                cuenta.CreditosDolares = (Decimal)TableWithAllAccountsBalance.AsEnumerable()
                    .Where(row => row.Field<UInt32>("account_id") == cuenta.Id)
                    .Sum(row => row.Field<Double>("SUM(credito_USD)"));
            }
        }
    }
}