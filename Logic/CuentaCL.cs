using AriesWebApi.Entities.Accounts; 
using System.Collections.Generic;
using AriesWebApi.Data.Daos;
using System;

namespace AriesWebApi.Logic
{
    public class CuentaCL
    {
        private readonly CuentaDao _cuentadao = new CuentaDao(); 
        public List<Cuenta> GetAll(string companyid) 
        => _cuentadao.GetAll(companyid); 

        public void Update(string companyId, Cuenta cuenta, int userId){
            _cuentadao.UpdateNameInfo(companyId,cuenta, userId); 
        }

        public void Delete(string companyId, double accountId, int userId)
        {
            _cuentadao.Delete(companyId, accountId, userId); 
        }
    }
}