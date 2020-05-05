using AriesWebApi.Entities.Accounts; 
using System.Collections.Generic;
using AriesWebApi.Data.Daos;

namespace AriesWebApi.Logic
{
    public class CuentaCL
    {
        private readonly CuentaDao _cuentadao = new CuentaDao(); 
        public List<Cuenta> GetAll(string companyid) 
        => _cuentadao.GetAll(companyid); 
    }
}