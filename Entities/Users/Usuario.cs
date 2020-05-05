using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Windows;

namespace AriesWebApi.Entities.Users {
    public class Usuario {
        
        [Required]
        public double UsuarioId { get; set; }
        
        [MaxLength (250)]
        [Required (AllowEmptyStrings = false)]
        [DisplayFormat (ConvertEmptyStringToNull = false)]
        public string UserName { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        
        [MaxLength (250)]
        [Required (AllowEmptyStrings = false)]
        [DisplayFormat (ConvertEmptyStringToNull = false)]
        public string MyCedula { get; set; }
        
        [MaxLength (250)]
        [Required (AllowEmptyStrings = false)]
        [DisplayFormat (ConvertEmptyStringToNull = false)]
        public string MyNombre { get; set; }
        public string MyApellidoPaterno { get; set; }
        public string MyApellidoMaterno { get; set; }

        [PhoneAttribute]
        public string MyTelefono { get; set; }

        [EmailAddressAttribute]
        public string MyMail { get; set; }
        public string MyNotas { get; set; }

        [DataType(DataType.Password)]
        public string MyClave { get; set; }
        public string UpdatedBy { get; set; }
        public bool MyActivo { get; set; }
        public DateTime MyFechaCreacion { get; set; }
        public DateTime MyFechaActualizacion { get; set; }
        public List<Modulo> Modulos { get; set; } = new List<Modulo> ();

        public Usuario (string myCedula, string myNombre, string username, TipoUsuario tipoUsuario, string myApellidoPaterno, string myApellidoMaterno,
            string myTelefono, string myMail, string myNotas, string myClave, bool myActivo,
            DateTime myFechaCreacion, DateTime myFechaActualizacion, double myID = 0, string updatedBy = "") {
            UsuarioId = myID;
            MyCedula = myCedula ??
                throw new ArgumentNullException ("CEDULA ");
            MyNombre = myNombre ??
                throw new ArgumentNullException ("NOMBRE");
            UserName = username;
            TipoUsuario = tipoUsuario;
            MyApellidoPaterno = myApellidoPaterno;
            MyApellidoMaterno = myApellidoMaterno;
            MyTelefono = myTelefono;
            MyMail = myMail;
            MyNotas = myNotas;
            MyClave = myClave ??
                throw new ArgumentNullException ("CLAVE");
            MyActivo = myActivo;
            MyFechaCreacion = myFechaCreacion;
            MyFechaActualizacion = myFechaActualizacion;
            UpdatedBy = updatedBy;
        }
        public Usuario (string myCedula, string myNombre, string username, TipoUsuario tipoUsuario, string myApellidoPaterno, string myApellidoMaterno,
            string myTelefono, string myMail, string myNotas, string myClave, bool myActivo, double myID = 0, string myUpdated = "") {
            UsuarioId = myID;
            MyCedula = myCedula ??
                throw new ArgumentNullException ("CEDULA ");
            MyNombre = myNombre ??
                throw new ArgumentNullException ("NOMBRE");
            UserName = username;
            TipoUsuario = tipoUsuario;
            MyApellidoPaterno = myApellidoPaterno;
            MyApellidoMaterno = myApellidoMaterno;
            MyTelefono = myTelefono;
            MyMail = myMail;
            MyNotas = myNotas;
            MyClave = myClave ??
                throw new ArgumentNullException ("CLAVE");
            MyActivo = myActivo;
            //MyAdmin = myAdmin;
            UpdatedBy = myUpdated;
        }
        public Usuario () { }
        public override string ToString () {
            return $"{MyNombre} {MyApellidoPaterno} {MyApellidoMaterno}";
        }

    }
}