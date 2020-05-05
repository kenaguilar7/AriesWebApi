using System;
using System.Collections.Generic;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.Users;

namespace AriesWebApi.Logic {
    public class UserCL {
        private readonly UsuarioDao _userDao = new UsuarioDao ();
        public List<Usuario> GetAll () => _userDao.GetAll ();
        public bool Insert (Usuario user) =>  _userDao.Insert (user); 
        public Boolean Update (Usuario user) => _userDao.Update (user);

    }
}