using System.Collections.Generic;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.security;

namespace AriesWebApi.Entities.Windows
{
    public class Modulo : IPermiso
    {
        public int Codigo { get; set; }
        public string NombreInterno { get; set; }
        public string NombreExterno { get; set; }
        public TipoUsuario TipoUsario { get; set; }
        public bool TienePermiso { get; set; }
        public List<Ventana> LstVentanas { get; set; } = new List<Ventana>();

        public Modulo(int codigo, string nombreInterno, string nombreExterno, 
                     TipoUsuario tipoUsario, bool activo, List<Ventana> lstVentanas)
        {
            Codigo = codigo;
            NombreInterno = nombreInterno;
            NombreExterno = nombreExterno;
            TipoUsario = tipoUsario;
            TienePermiso = activo;
            LstVentanas = lstVentanas;
        }

        public Modulo()
        {
        }
        public override string ToString()
        {
            return NombreExterno; 
        }
    }
}
