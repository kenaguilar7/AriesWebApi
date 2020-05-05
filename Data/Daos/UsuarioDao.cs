using System;
using System.Collections.Generic;
using System.Data;
using AriesWebApi.Data.Connection;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.security;
using AriesWebApi.Entities.Users;
using MySql.Data.MySqlClient;

namespace AriesWebApi.Data.Daos
{
    public class UsuarioDao
    {
        private readonly Manejador manejador = new Manejador();
        public bool Insert(Usuario userInsert)
        {
            // if (!Guachi.Consultar(userTrigger, VentanaInfo.FormMaestroUsuario, CRUDName.Insertar))
            // {
            //     // mensaje = "Acceso denegado!!!";
            //     return false;
            // }
            
            //Este query debe devolver el id del usuario insertado
            var sql = "INSERT INTO users(number_id,  name,  user_name , user_type , lastname_p , lastname_m,  phone_number , mail , notes,  password,  updated_by,  active) " +
                                 "VALUES(@number_id, @name, @user_name, @user_type, @lastname_p, @lastname_m, @phone_number, @mail, @notes, @password, @updated_by, @active);";
            List<Parametro> lst = new List<Parametro>();
            try
            {

                lst.Add(new Parametro("@number_id", userInsert.MyCedula));
                lst.Add(new Parametro("@name", userInsert.MyNombre));
                lst.Add(new Parametro("@user_name", userInsert.UserName));
                lst.Add(new Parametro("@user_type", (int)userInsert.TipoUsuario));
                lst.Add(new Parametro("@lastname_p", userInsert.MyApellidoPaterno));
                lst.Add(new Parametro("@lastname_m", userInsert.MyApellidoMaterno));
                lst.Add(new Parametro("@phone_number", userInsert.MyTelefono));
                lst.Add(new Parametro("@mail", userInsert.MyMail));
                lst.Add(new Parametro("@notes", userInsert.MyNotas));
                lst.Add(new Parametro("@password", userInsert.MyClave));
                lst.Add(new Parametro("@updated_by", userInsert.UpdatedBy));
                lst.Add(new Parametro("@active", Convert.ToInt16(userInsert.MyActivo)));

                if (manejador.Ejecutar(sql, lst, CommandType.Text) == 0)
                {
                    // mensaje = "No se guardaron datos";
                    return false;
                }
                else
                {
                    userInsert.UsuarioId = 666; 
                    // mensaje = "Datos guardados correctamente";
                    return true;
                }

            }
            catch (Exception)
            {
                // mensaje = ex.Message;
                throw; 
            }
        }
        public Boolean Update(Usuario user)
        {
            // if (!Guachi.Consultar(user, VentanaInfo.FormMaestroUsuario, CRUDName.Actualizar))
            // {
            //     // mensaje = "Acceso denegado!!!";
            //     return false;
            // }

            try
            {
                var sql = "UPDATE users SET number_id = @number_id,name=@name,user_name=@user_name,user_type=@user_type,lastname_p=@lastname_p,lastname_m=@lastname_m,phone_number=@phone_number," +
                      "mail=@mail,notes=@notes,password=@password,updated_by=@updated_by,active=@active WHERE user_id = @user_id;";
                List<Parametro> lst = new List<Parametro>();

                lst.Add(new Parametro("@user_id", user.UsuarioId));
                lst.Add(new Parametro("@number_id", user.MyCedula));
                lst.Add(new Parametro("@name", user.MyNombre));
                lst.Add(new Parametro("@user_name", user.UserName));
                lst.Add(new Parametro("@user_type", (int)user.TipoUsuario));
                lst.Add(new Parametro("@lastname_p", user.MyApellidoPaterno));
                lst.Add(new Parametro("@lastname_m", user.MyApellidoMaterno));
                lst.Add(new Parametro("@phone_number", user.MyTelefono));
                lst.Add(new Parametro("@mail", user.MyMail));
                lst.Add(new Parametro("@notes", user.MyNotas));
                lst.Add(new Parametro("@password", user.MyClave));
                lst.Add(new Parametro("@updated_by", user.UpdatedBy));
                lst.Add(new Parametro("@active", Convert.ToInt16(user.MyActivo)));
                
                return (manejador.Ejecutar(sql, lst, CommandType.Text) == 0)? true:false; 

            }
            catch (Exception)
            {
                throw; 
            }

        }
        public List<Usuario> GetAll()
        {
            //if (!Guachi.Consultar(usuario, VentanaInfo.FormMaestroUsuario, CRUDName.Listar))
            //{
            //    mensaje = "Acceso denegado!!!";
            //    return false;
            //}

            List<Usuario> retorno = new List<Usuario>();

            var sql = "SELECT " +
                "user_id, " +            ///0
                "user_name, " +          ///1
                "user_type+0, " +        ///2
                "number_id, " +          ///3
                "name, " +               ///4
                "lastname_p, " +         ///5
                "lastname_m, " +         ///6
                "phone_number, " +       ///7
                "mail, " +               ///8
                "notes, " +              ///9
                "created_at, " +         ///10
                "updated_at, " +         ///11
                "updated_by," +         ///12
                "active, " +              ///13
                "password " +             ///14
                "FROM users";

            var dt = manejador.Listado(sql, CommandType.Text);

            foreach (DataRow item in dt.Rows)
            {
                Object[] vs = item.ItemArray;

               retorno.Add(new Usuario(
                    myID: Convert.ToDouble(vs[0]),                                       ///"user_id, " +            ///0   
                    username: Convert.ToString(vs[1]),                                   ///"user_name, " +          ///1
                    tipoUsuario: (TipoUsuario)Convert.ToInt16(vs[2]),                    ///"user_type+0, " +        ///2
                    myCedula: Convert.ToString(vs[3]),                                   ///"number_id, " +          ///3
                    myNombre: Convert.ToString(vs[4]),                                   ///"name, " +               ///4
                    myApellidoPaterno: Convert.ToString(vs[5]),                          ///"lastname_p, " +         ///5
                    myApellidoMaterno: Convert.ToString(vs[6]),                          ///"lastname_m, " +         ///6
                    myTelefono: Convert.ToString(vs[7]),                                 ///"phone_number, " +       ///7
                    myMail: Convert.ToString(vs[8]),                                     ///"mail, " +               ///8
                    myNotas: Convert.ToString(vs[9]),                                    ///"notes, " +              ///9
                   /* myAdmin: Convert.ToBoolean(vs[10]),            */                      ///"admin, " +              ///10
                    myFechaCreacion: Convert.ToDateTime(vs[10]),                         ///"created_at, " +         ///11
                    myFechaActualizacion: Convert.ToDateTime(Convert.ToString(vs[11])),  ///"updated_at, " +         ///12
                    updatedBy: Convert.ToString(vs[12]),               ///"updated_by ," +         ///13
                    myActivo: Convert.ToBoolean(vs[13]),                                 ///"active, "+              ///14
                    myClave: Convert.ToString(vs[14])                                    ///"password "+             ///15
                                       ));
            }

            return retorno;
        }
        public DataTable Login(string user, string clave)
        {
            var sql = "SELECT " +
                "user_id, " +
                "user_name, " +
                "user_type+0, " +
                "number_id, " +
                "name, " +
                "lastname_p, " +
                "lastname_m, " +
                "phone_number, " +
                "mail, " +
                "notes, " +
                "created_at, " +
                "updated_at, " +
                "updated_by ," +
                "active, " +
                "password " +
                "FROM users WHERE user_name = @user_name AND password = @password AND active = 1";

            return manejador.Listado(sql,
                                        new List<Parametro>{ new Parametro("@user_name", user),
                                                             new Parametro("@password", clave)}, CommandType.Text);
        }
        /// <summary>
        /// Retorna true si el nombre existe ya en la base de datos
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Boolean VerificarNombre(string userName)
        {

            var retorno = false;
            try
            {
                var sql = "SELECT COUNT(*) FROM users WHERE user_name = @user_name";

                using (MySqlCommand cmd = new MySqlCommand(sql, manejador.GetConnection()))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter
                    {
                        SelectCommand = cmd
                    };

                    cmd.Parameters.AddWithValue("@user_name", userName);


                    retorno = Convert.ToBoolean(cmd.ExecuteScalar());
                }


                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
