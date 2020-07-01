using AriesWebApi.Entities.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AriesWebApi.Entities.Accounts.AccountList
{
    public static class ManageAccountListExtension
    {

        public static IEnumerable<Cuenta> GetLowLevelAccounts(this IEnumerable<Cuenta> cuentas, double accountId)
        {
            List<Cuenta> retorno = new List<Cuenta>();
            var lstaccounts = cuentas.Where(x => x.Padre == accountId || x.Id == accountId);

            retorno.AddRange(lstaccounts);

            foreach (Cuenta cuenta in lstaccounts)
            {
                if (cuenta.Id != accountId)
                {
                    var lowLevelOfAccountList = cuentas.Where(x => x.Padre == cuenta.Id);
                    retorno.AddRange(GetLowLevelAccounts(lowLevelOfAccountList, cuenta.Id));
                }
            }

            return retorno;
        }

        public static IEnumerable<Cuenta> GetHightLevelAccounts(this IEnumerable<Cuenta> cuentas, double accountId)
        {
            List<Cuenta> retorno = new List<Cuenta>();
            var cuentaPrincipal = cuentas.First(row => row.Id == accountId);

            var lstaccounts = cuentas.Where(x => x.Id == cuentaPrincipal.Padre || x.Id == accountId);

            retorno.AddRange(lstaccounts);

            foreach (Cuenta cuenta in lstaccounts)
            {
                if (cuenta.Id != accountId)
                {
                    var upLevelOfAccountList = cuentas.Where(x => x.Id == cuenta.Padre);
                    retorno.AddRange(GetLowLevelAccounts(upLevelOfAccountList, cuenta.Id));
                }
            }

            return retorno;
        }

        public static IEnumerable<Cuenta> BuildAccountsBalance(this IEnumerable<Cuenta> cuentas)
        {
            var lstLowerLevel = cuentas.Where(row => row.Indicador == IndicadorCuenta.Cuenta_Auxiliar);

            foreach (Cuenta lowCuenta in lstLowerLevel)
            {
                GetAndSumAllAccountsFromLowerToHigherLevel(ref cuentas, lowCuenta);
            }

            return cuentas;
        }

        private static void GetAndSumAllAccountsFromLowerToHigherLevel(ref IEnumerable<Cuenta> cuentas,
                                                                                      Cuenta cuentaConSaldo)
        {
            Cuenta cuentaAux = cuentaConSaldo;

            do
            {
                cuentaAux = cuentas.FirstOrDefault(x => x.Id == cuentaAux.Padre);

                if (cuentaAux != null)
                    SumBalance(cuentaConSaldo, cuentaAux);

            } while (cuentaAux != null);
        
        }

        private static void SumBalance(Cuenta cuentaSumFrom, Cuenta cuentaSumTo)
        {
            cuentaSumTo.DebitosColones += cuentaSumFrom.DebitosColones;
            cuentaSumTo.CreditosColones += cuentaSumFrom.CreditosColones;
            cuentaSumTo.DebitosDolares += cuentaSumFrom.DebitosDolares;
            cuentaSumTo.CreditosDolares += cuentaSumFrom.CreditosDolares;
        }
    }
}
