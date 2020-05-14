using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace AriesWebApi.Entities.Companies {
    public class CompanyTypeConverter : JsonConverter {
        public override bool CanConvert (Type objectType) => true;
        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject jo = JObject.Load (reader);
            Compañia _company;

            //Cédula fisica
            if (jo["tipoId"].Value<int> () == 1) {
                _company = new PersonaJuridica ();
            } else {
                _company = new PersonaFisica ();
            }
            serializer.Populate (jo.CreateReader (), _company);
            return _company;
        }

        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) {
            // var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            //use the default serialization - it works fine
            serializer.Serialize (writer, value);
        }
    }
}