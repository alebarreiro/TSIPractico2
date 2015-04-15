using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Entities;

namespace DataAccessLayer.Transformers
{

    public abstract class AbstractJsonConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            T target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected static bool FieldExists(JObject jObject, string name, JTokenType type)
        {
            JToken token;
            return jObject.TryGetValue(name, out token);
        }

    }



    public class EmployeeConverter : AbstractJsonConverter<Employee>
    {

        protected override Employee Create(Type objectType, JObject jObject)
        {
            if (FieldExists(jObject, "HourlyDate", JTokenType.Integer))
                return new PartTimeEmployee();
            if (FieldExists(jObject, "Salary", JTokenType.Integer))
                return new FullTimeEmployee();

            throw new InvalidOperationException();
        }

    }

}
