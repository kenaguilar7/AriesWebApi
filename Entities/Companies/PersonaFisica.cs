using System.Runtime.Serialization;
using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Companies
{
    public class PersonaFisica : Compañia
    {
        public override string _tipoCompany { get; set; } = "fisica"; 
        public PersonaFisica()
        {
        }

        public PersonaFisica(TipoID tipoID, string numeroId, string nombre, TipoMonedaCompañia TipoMoneda,  string direccion,
                                string[] telefono, string web, string correo, string observaciones, string apellidoPaterno, string apellidoMaterno, string codigo = "", bool activo = true) :
                                base(tipoID, numeroId, nombre, TipoMoneda, direccion, telefono, web, correo, observaciones, codigo, activo)
        {
            this.MyApellidoPaterno = apellidoPaterno;
            this.MyApellidoMaterno = apellidoMaterno;
        }
        public PersonaFisica(string apelledoPaterno, string apellidoMaterno)
        {
            this.MyApellidoPaterno = apelledoPaterno;
            this.MyApellidoMaterno = apellidoMaterno;
        }

        public string MyApellidoPaterno {get;set;}
        public string MyApellidoMaterno {get;set;}
        
    }
}