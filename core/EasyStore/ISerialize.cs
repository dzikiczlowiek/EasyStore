namespace EasyStore
{
    using System;
    using System.IO;

    public interface ISerialize
    {
        object Deserialize(Type type, Stream input);

        T Deserialize<T>(Stream input);

        void Serialize<T>(Stream output, T payload);
    }
}
