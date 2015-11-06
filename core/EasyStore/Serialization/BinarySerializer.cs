namespace EasyStore.Serialization
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public class BinarySerializer : ISerialize
    {
        private readonly IFormatter _formatter = new BinaryFormatter();

        public virtual void Serialize<T>(Stream output, T graph)
        {
            this._formatter.Serialize(output, graph);
        }

        public virtual T Deserialize<T>(Stream input)
        {
            return (T)this._formatter.Deserialize(input);
        }

        public object Deserialize(Type type, Stream input)
        {
            return this._formatter.Deserialize(input);
        }
    }
}
