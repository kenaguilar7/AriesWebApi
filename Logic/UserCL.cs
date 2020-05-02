using System;
using System.Collections.Generic;
using AriesWebApi.Data.Daos;
using AriesWebApi.Entities.Users;

namespace AriesWebApi.Logic {
    public class UserCL {
        private readonly UsuarioDao _userDao = new UsuarioDao ();
        public List<Usuario> GetAll () => _userDao.GetAll ();

        public bool Insert (Usuario userInsert) {

            try {

                if (string.IsNullOrWhiteSpace (userInsert.MyNombre) || string.IsNullOrWhiteSpace (userInsert.UserName)) {
                    // mensaje = "No se puede guardar usuarios con nombes en blanco";
                    return false;
                } else {
                    if (_userDao.VerificarNombre (userInsert.UserName)) //si es true el usuario existe
                    {
                        // mensaje = "El nombre de usuario ya se encuentra registrado, intente con otro";
                        return false;
                    }

                    return (_userDao.Insert (userInsert)) ? true : false;

                }
            } catch (Exception) {
                // mensaje = ex.Message;
                throw; 
            }

        }

    }
}