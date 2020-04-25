using AriesWebApi.Entities.Enums;

namespace AriesWebApi.Entities.Companies
{
    public class PersonaFisica : Compañia
    {
        private string apellidoPaterno;
        private string apellidoMaterno;


        public PersonaFisica()
        {
        }

        public PersonaFisica(TipoID tipoID, string numeroId, string nombre, TipoMonedaCompañia TipoMoneda,  string direccion,
                                string[] telefono, string web, string correo, string observaciones, string apellidoPaterno, string apellidoMaterno, string codigo = "", bool activo = true) :
                                base(tipoID, numeroId, nombre, TipoMoneda, direccion, telefono, web, correo, observaciones, codigo, activo)
        {
            this.apellidoPaterno = apellidoPaterno;
            this.apellidoMaterno = apellidoMaterno;
        }
        public PersonaFisica(string apelledoPaterno, string apellidoMaterno)
        {
            this.apellidoPaterno = apelledoPaterno;
            this.MyApellidoMaterno = apellidoMaterno;
        }

        public string MyApellidoPaterno
        {
            get { return apellidoPaterno; }
            set { apellidoPaterno = value; }
        }

        public string MyApellidoMaterno
        {
            get { return apellidoMaterno; }
            set { apellidoMaterno = value; }
        }
    }
}