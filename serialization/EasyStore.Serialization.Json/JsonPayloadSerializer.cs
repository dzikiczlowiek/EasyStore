namespace EasyStore.Serialization.Json
{
    using System;
    using System.IO;
    using System.Text;

    using Newtonsoft.Json;

    public class JsonPayloadSerializer : ISerialize
    {
        private readonly JsonSerializer _serializer = 
            new JsonSerializer
            {
                ContractResolver = new JsonContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            };

        public JsonPayloadSerializer()
        {
            this._serializer.Converters.Add(new AggregateRootJsonConverter());
        }

        public object Deserialize(Type type, Stream input)
        {
            using (var streamReader = new StreamReader(input, Encoding.UTF8))
            {
                return this.Deserialize(type, new JsonTextReader(streamReader));
            }
        }

        public T Deserialize<T>(Stream input)
        {
            using (var streamReader = new StreamReader(input, Encoding.UTF8))
            {
                return this.Deserialize<T>(new JsonTextReader(streamReader));
            }
        }
       
        public void Serialize<T>(Stream output, T payload)
        {
            using (var streamWriter = new StreamWriter(output, Encoding.UTF8))
            {
                this.Serialize(new JsonTextWriter(streamWriter), payload);
            }
        }

        protected virtual T Deserialize<T>(JsonReader reader)
        {
            return (T)this.Deserialize(typeof(T), reader);
        }

        protected virtual object Deserialize(Type type, JsonReader reader)
        {
            using (reader)
            {
                return this._serializer.Deserialize(reader, type);
            }
        }
        
        protected virtual void Serialize(JsonWriter writer, object payload)
        {
            using (writer)
            {
                this._serializer.Serialize(writer, payload);
            }
        }
    }
}