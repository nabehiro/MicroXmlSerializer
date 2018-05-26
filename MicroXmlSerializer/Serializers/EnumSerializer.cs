using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace MicroXmlSerializer.Serializers
{
    class EnumSerializer : IEntitySerializer
    {
        public Type TargetType { get; }

        private readonly Dictionary<string, object> _deserializeMap = new Dictionary<string, object>();

        public EnumSerializer(Type type)
        {
            TargetType = type;
            if (!TargetType.IsEnum)
                throw new ArgumentException($"Type is not enum. type:{TargetType}");

            foreach (var value in Enum.GetValues(TargetType))
            {
                var fieldInfo = TargetType.GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);
                if (fieldInfo.GetCustomAttribute(typeof(XmlEnumAttribute), false) is XmlEnumAttribute attr)
                {
                    _deserializeMap[attr.Name] = value;
                }
                else
                {
                    var underlyingValue = Convert.ChangeType(value, Enum.GetUnderlyingType(TargetType));
                    _deserializeMap[underlyingValue.ToString()] = value;
                    _deserializeMap[value.ToString()] = value;
                }
            }
        }

        public object Deserialize(string str, string format)
        {
            if (_deserializeMap.TryGetValue(str, out var value))
                return value;
            else
                throw new FormatException($"str can not be deserialized. str:{str}, type:{TargetType}");
        }

        public string Serialize(object obj, string format)
        {
            throw new NotImplementedException();
        }
    }
}
