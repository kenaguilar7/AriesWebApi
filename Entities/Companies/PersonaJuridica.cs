using System.Runtime.Serialization;
using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Companies
{
    
    public class PersonaJuridica : Compañia
    {

        public override string tipoCompany {get;set;} = "juridica"; 
        public PersonaJuridica()
        {
        }

        public PersonaJuridica(TipoID tipoID, string numeroId, string nombre, TipoMonedaCompañia TipoMoneda, string direccion,
                                  string[] telefono, string web, string correo, string observaciones,
                                  string representanteLegal, string IDRepresentante, string codigo = "", bool activo = true) :
                                  base(tipoID, numeroId, nombre, TipoMoneda, direccion, telefono, web, correo, observaciones,codigo, activo)
        {
            this.MyRepresentanteLegal = representanteLegal;
            this.MyIDRepresentanteLegal = IDRepresentante;
        }

        public string MyRepresentanteLegal {get;set;}
        public string MyIDRepresentanteLegal {get;set;}
        
    }
}