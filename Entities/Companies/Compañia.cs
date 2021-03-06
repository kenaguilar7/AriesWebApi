using AriesWebApi.Entities.Enums;
using JsonSubTypes;
using Newtonsoft.Json;

namespace AriesWebApi.Entities.Companies {
    // [JsonConverter(typeof(CompanyTypeConverter))]
    [JsonConverter (typeof (JsonSubtypes), "_tipoCompany")]
    [JsonSubtypes.KnownSubType (typeof (PersonaFisica), "fisica")]
    [JsonSubtypes.KnownSubType (typeof (PersonaJuridica), "juridica")]
    public  class Compañia {
        
        public virtual string _tipoCompany { get; set; }
        public string Codigo { get; set; }
        public TipoID TipoId { get; set; }
        public string NumeroCedula { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Web { get; set; }
        public string Correo { get; set; }
        public string[] Telefono { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public TipoMonedaCompañia TipoMoneda { get; set; }
        public Compañia () { }

        protected Compañia (TipoID tipoID, string numeroId, string nombre, TipoMonedaCompañia TipoMoneda, string direccion,
            string[] telefono, string web, string correo, string observaciones, string codigo = "", bool activo = true) {
            this.Codigo = codigo;
            this.TipoId = tipoID;
            this.NumeroCedula = numeroId;
            this.Nombre = nombre;
            this.TipoMoneda = TipoMoneda;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.Web = web;
            this.Correo = correo;
            this.Observaciones = observaciones;
            this.Activo = activo;
        }

        public override string ToString () {
            return $"{ Nombre.ToUpper()}-{Codigo}";
        }
    }
}