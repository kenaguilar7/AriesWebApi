using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Companies
{
    public class PersonaJuridica : Compañia
    {
        private string representanteLegal;
        private string IDRepresentante;
        public PersonaJuridica()
        {
        }

        public PersonaJuridica(TipoID tipoID, string numeroId, string nombre, TipoMonedaCompañia TipoMoneda, string direccion,
                                  string[] telefono, string web, string correo, string observaciones,
                                  string representanteLegal, string IDRepresentante, string codigo = "", bool activo = true) :
                                  base(tipoID, numeroId, nombre, TipoMoneda, direccion, telefono, web, correo, observaciones,codigo, activo)
        {
            this.representanteLegal = representanteLegal;
            this.IDRepresentante = IDRepresentante;
        }

        public string MyRepresentanteLegal
        {
            get { return representanteLegal; }
            set { representanteLegal = value; }
        }

        public string MyIDRepresentanteLegal
        {
            get { return IDRepresentante; }
            set { IDRepresentante = value; }
        }
    }
}