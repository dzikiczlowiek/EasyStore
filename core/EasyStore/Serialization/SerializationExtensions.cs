namespace EasyStore.Serialization
{
    using System;
using System.IO;

    public static class SerializationExtensions
    {
        public static byte[] Serialize<T>(this ISerialize serializer, T value)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, value);
                return stream.ToArray();
            }
        }

        public static T Deserialize<T>(this ISerialize serializer, byte[] serialized)
        {
            serialized = serialized ?? new byte[] { };
            if (serialized.Length == 0)
            {
                return default(T);
            }

            using (var stream = new MemoryStream(serialized))
            {
                return serializer.Deserialize<T>(stream);
            }
        }

        public static object Deserialize(this ISerialize serializer, Type type, byte[] serialized)
        {
            serialized = serialized ?? new byte[] { };
            if (serialized.Length == 0)
            {
                // todo: change behavior
                return null;
            }

            using (var stream = new MemoryStream(serialized))
            {
                return serializer.Deserialize(type, stream);
            }
        }
    }
}
