using MicroXmlSerializer.Serializers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MicroXmlSerializer
{
    class EntitySerializerProvider
    {
        private readonly Dictionary<Type, IEntitySerializer> _serializers = new Dictionary<Type, IEntitySerializer>();
        private readonly ConcurrentDictionary<Type, IEntitySerializer> _enumSerializers = new ConcurrentDictionary<Type, IEntitySerializer>();

        public EntitySerializerProvider()
        {
            new List<Type>
            {
                typeof(char),
                typeof(string),
                typeof(sbyte),
                typeof(byte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(bool)
            }.ForEach(t => AddSerializer(t, new EntitySerializer(t)));

            AddSerializer(typeof(DateTime), new DateTimeSerializer());
        }

        public void AddSerializer(Type type, IEntitySerializer serializer)
        {
            _serializers[type] = _serializers[type] = serializer;
            if (type.IsValueType)
            {
                var nullableType = typeof(Nullable<>).MakeGenericType(type);
                _serializers[nullableType] = new NullableEntitySerializer(serializer);
            }
        }

        public bool IsSerializable(Type type)
        {
            return _serializers.ContainsKey(type) || type.IsEnum || Nullable.GetUnderlyingType(type)?.IsEnum == true;
        }

        public IEntitySerializer GetSerizliser(Type type)
        {
            if (_serializers.TryGetValue(type, out var serializer))
                return serializer;
            else if (type.IsEnum || Nullable.GetUnderlyingType(type)?.IsEnum == true)
            {
                if (_enumSerializers.TryGetValue(type, out var enumSerializer))
                    return enumSerializer;

                if (type.IsEnum)
                    enumSerializer = new EnumSerializer(type);
                else
                    enumSerializer = new NullableEntitySerializer(new EnumSerializer(Nullable.GetUnderlyingType(type)));

                _enumSerializers[type] = enumSerializer;
                return enumSerializer;
            }
            else
                return null;
        }
    }
}
