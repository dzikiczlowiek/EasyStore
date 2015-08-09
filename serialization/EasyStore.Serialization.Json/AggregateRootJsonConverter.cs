namespace EasyStore.Serialization.Json
{
    using System;
    using System.Reflection;

    using EasyStore.CommonDomain;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class AggregateRootJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(AggregateRoot).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            AggregateRoot target;
            var jObject = JObject.Load(reader);

            var constructor = objectType.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Guid) }, null);

            if (jObject.Type == JTokenType.Null)
            {
                target = null;
            }
            else
            {
                var id = jObject["Id"].ToObject<Guid>();
                target = constructor.Invoke(new object[] { id }) as AggregateRoot;
            }

            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}
