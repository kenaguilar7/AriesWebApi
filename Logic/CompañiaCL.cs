using System;
using System.Collections.Generic;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Users;
using AriesWebApi.Entities.Verifications;

namespace AriesWebApi.Logic
{
    public class CompañiaCL
    {

        private readonly CompañiaDao _companyDao = new CompañiaDao();

        public IEnumerable<Compañia> Get()
        {
            var user = new Usuario()
            {
                UsuarioId = 1
            };
            return _companyDao.GetAll(user);
        }

        public Compañia Insert(Compañia company, Usuario user, string copyDataFrom)
            => _companyDao.Insert(company, copyDataFrom, user);

        public void Update(Compañia company, Usuario user)
        {
            _companyDao.Update(company, user);

        }

        public string NuevoCodigo()
        {
            return _companyDao.NuevoCodigo();
        }

        public IEnumerable<Compañia> GetAll(Usuario usuario)
        {
            return _companyDao.GetAll(usuario);
        }
    }
}